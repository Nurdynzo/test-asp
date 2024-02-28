using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;
using System.Linq;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientWallet.Abstractions;

namespace Plateaumed.EHR.PatientWallet.Command;

public class CreateWalletRefundCommandHandler : ICreateWalletRefundCommandHandler
{
    private readonly IRepository<InvoiceRefundRequest,long> _invoiceRefundRequestRepository;
    private readonly IAbpSession _abpSession;
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepository ;

    public CreateWalletRefundCommandHandler(IRepository<InvoiceRefundRequest, long> invoiceRefundRequestRepository,
        IAbpSession abpSession, 
        IRepository<InvoiceItem, long> invoiceItemRepository,
        IUnitOfWorkManager unitOfWorkManager,
        IRepository<PaymentActivityLog, long> paymentActivityLogRepository)
    {
        _invoiceRefundRequestRepository = invoiceRefundRequestRepository;
        _abpSession = abpSession;
        _invoiceItemRepository = invoiceItemRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _paymentActivityLogRepository = paymentActivityLogRepository;
    }

    public async Task Handle(InvoiceWalletRefundRequest request, long facilityId)
    {
        ValidateRequest(request);

        var invoiceRefundRequests = await QueryItemsToRefund(request);
        ThrowExceptionIfNoMatchingItems(invoiceRefundRequests);

        using var unitOfWork = _unitOfWorkManager.Begin();
        foreach (var invoiceRefundRequest in invoiceRefundRequests)
        {
            await LogRefundRequest(request, facilityId, invoiceRefundRequest);

            await LogPaymentActivity(request, invoiceRefundRequest);
        }
        await _unitOfWorkManager.Current.SaveChangesAsync();
        await unitOfWork.CompleteAsync();
    }

    private async Task<List<InvoiceItem>> QueryItemsToRefund(InvoiceWalletRefundRequest request)
    {
        var query = from ii in _invoiceItemRepository.GetAll()
            where request.InvoiceItemsIds.Contains(ii.Id) &&
                  ii.Status == InvoiceItemStatus.Paid
            select ii;
        var invoiceRefundRequests = await query.ToListAsync();
        return invoiceRefundRequests;
    }

    private static void ThrowExceptionIfNoMatchingItems(List<InvoiceItem> invoiceRefundRequests)
    {
        if (invoiceRefundRequests.Count == 0)
        {
            throw new UserFriendlyException("No matching items to refund");
        }
    }

    private static void ValidateRequest(InvoiceWalletRefundRequest request)
    {
        if (request.InvoiceItemsIds == null || request.InvoiceItemsIds.Length == 0)
        {
            throw new UserFriendlyException("invoice items cannot be empty");
        }
    }

    private async Task LogPaymentActivity(InvoiceWalletRefundRequest request, InvoiceItem invoiceRefundRequest)
    {
        var paymentActivityLog = new PaymentActivityLog
        {
            PatientId = request.PatientId,
            InvoiceId = invoiceRefundRequest.InvoiceId,
            InvoiceItemId = invoiceRefundRequest.Id,
            TransactionAction = TransactionAction.RequestWalletFunding,
            TenantId = _abpSession.TenantId.GetValueOrDefault(),
        };
        await _paymentActivityLogRepository.InsertAsync(paymentActivityLog);
    }

    private async Task LogRefundRequest(InvoiceWalletRefundRequest request, long facilityId,
        InvoiceItem invoiceRefundRequest)
    {
        var walletFundingRequest = new InvoiceRefundRequest
        {
            PatientId = request.PatientId,
            InvoiceId = invoiceRefundRequest.InvoiceId,
            InvoiceItemId = invoiceRefundRequest.Id,
            Status = InvoiceRefundStatus.Pending,
            FacilityId = facilityId,
            TenantId = _abpSession.TenantId.GetValueOrDefault()
        };
        await _invoiceRefundRequestRepository.InsertAsync(walletFundingRequest);
    }
}