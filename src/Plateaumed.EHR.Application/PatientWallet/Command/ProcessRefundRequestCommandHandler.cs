using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;
using  Plateaumed.EHR.Utility;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.PatientWallet.Command;

public class ProcessRefundRequestCommandHandler : IProcessRefundRequestCommandHandler
{

    private readonly IRepository<InvoiceRefundRequest,long> _invoiceRefundRequestRepository;
    private readonly IRepository<WalletHistory,long> _walletHistoryRepository;
    private readonly IRepository<Wallet,long> _walletRepository;
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepository;
    private readonly IAbpSession _abpSession;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public ProcessRefundRequestCommandHandler(
        IRepository<InvoiceRefundRequest, long> invoiceRefundRequestRepository,
        IRepository<WalletHistory, long> walletHistoryRepository, 
        IRepository<Wallet, long> walletRepository,
        IRepository<PaymentActivityLog, long> paymentActivityLogRepository,
        IAbpSession abpSession, 
        IUnitOfWorkManager unitOfWorkManager)
    {
        _invoiceRefundRequestRepository = invoiceRefundRequestRepository;
        _walletHistoryRepository = walletHistoryRepository;
        _walletRepository = walletRepository;
        _paymentActivityLogRepository = paymentActivityLogRepository;
        _abpSession = abpSession;
        _unitOfWorkManager = unitOfWorkManager;
    }


    public async Task Handle(ProcessRefundRequestCommand request, long facilityId)
    {
        var invoices = await _invoiceRefundRequestRepository.
            GetAll().Include(x=>x.InvoiceItem)
            .Include(x=>x.Invoice)
            .Where(x => x.PatientId == request.PatientId
                        && x.FacilityId == facilityId
                        && x.Status == InvoiceRefundStatus.Pending).ToListAsync();
        
        if (invoices.Count == 0)
        {
            throw new UserFriendlyException("No pending refund requests found for the patient");
        }
        using var unitOfWork = _unitOfWorkManager.Begin();
        if (request.IsApproved)
        {
            await ApprovedRefundRequest(request, facilityId, invoices);
        }
        else
        {
            await ProcessRefundRequest(facilityId, invoices, request.IsApproved);
        }
        await _unitOfWorkManager.Current.SaveChangesAsync();
        await unitOfWork.CompleteAsync();
       
    }

    private async Task ApprovedRefundRequest(ProcessRefundRequestCommand request, long facilityId, List<InvoiceRefundRequest> invoices)
    {
        var amountToRefund = invoices.Sum(x => x.InvoiceItem.AmountPaid);

        //applied deduction to refund amount
        amountToRefund -= (amountToRefund * Constants.RefundDeductionPercentage / 100);
        if (amountToRefund != request.TotalAmountToRefund.ToMoney())
        {
            throw new UserFriendlyException("Refund amount does not match the total amount to refund");
        }

        
        await ProcessRefundRequest(facilityId, invoices, request.IsApproved);
        await UpdatePatientWallet(request, facilityId, amountToRefund);
    }

    private async Task UpdatePatientWallet(ProcessRefundRequestCommand request, long facilityId, Money amountToRefund)
    {
        var wallet = _walletRepository.GetAll().FirstOrDefault(x => x.PatientId == request.PatientId);
        if (wallet == null)
        {
            throw new UserFriendlyException("Wallet not found for the patient");
        }
        wallet.Balance += amountToRefund;

        var walletHistory = new WalletHistory
        {
            Amount = amountToRefund,
            Narration = $"Refund",
            PatientId = request.PatientId,
            Source = TransactionSource.Indirect,
            Status = TransactionStatus.Approved,
            FacilityId = facilityId,
            WalletId = wallet.Id,
            CurrentBalance = wallet.Balance,
            TenantId = _abpSession.TenantId.GetValueOrDefault()
        };

       
        await _walletRepository.UpdateAsync(wallet);
        await _walletHistoryRepository.InsertAsync(walletHistory);
    }

    private async Task ProcessRefundRequest(long facilityId, List<InvoiceRefundRequest> invoices, bool isApproved)
    {
        foreach (var invoiceItem in invoices)
        {
            invoiceItem.Status = isApproved ? InvoiceRefundStatus.Approved : InvoiceRefundStatus.Rejected;
            if (invoiceItem.InvoiceItem != null)
            {
                invoiceItem.InvoiceItem.Status = isApproved ? InvoiceItemStatus.Refunded : InvoiceItemStatus.Paid;
                await _invoiceRefundRequestRepository.UpdateAsync(invoiceItem);

                var paymentTransaction = new PaymentActivityLog
                {
                    InvoiceNo = invoiceItem.Invoice?.InvoiceId,
                    FacilityId = facilityId,
                    Narration = isApproved ? "Refund Approved" : "Refund Rejected",
                    PatientId = invoiceItem.PatientId,
                    AmountRefund = invoiceItem.InvoiceItem.AmountPaid,
                    InvoiceId = invoiceItem.InvoiceId,
                    TransactionAction = isApproved ? TransactionAction.ApproveRefund : TransactionAction.RejectRefund,
                    TenantId = _abpSession.TenantId.GetValueOrDefault(),
                    InvoiceItemId = invoiceItem.InvoiceItemId
                };
                await _paymentActivityLogRepository.InsertAsync(paymentTransaction);
            }
        }
    }
}