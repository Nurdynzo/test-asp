using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Command;

public class ProcessInvoiceCancelRequestCommandHandler : IProcessInvoiceCancelRequestCommandHandler
{
    private readonly IRepository<InvoiceCancelRequest, long> _invoiceCancelRequestRepository;
    private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public ProcessInvoiceCancelRequestCommandHandler(
        IRepository<InvoiceCancelRequest, long> invoiceCancelRequestRepository,
        IRepository<PaymentActivityLog, long> paymentActivityLogRepository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _invoiceCancelRequestRepository = invoiceCancelRequestRepository;
        _paymentActivityLogRepository = paymentActivityLogRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(ApproveCancelInvoiceCommand request, long facilityId)
    {
        var query = await _invoiceCancelRequestRepository.GetAll()
            .Include(x => x.InvoiceItem)
            .Include(x => x.Invoice)
            .Where(x => x.PatientId == request.PatientId
                        && x.FacilityId == facilityId
                        && x.Status == InvoiceCancelStatus.Pending)
            .ToListAsync();
        using var unitOfWork = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew);
        foreach (var item in query)
        {
            await UpdateInvoiceCancelRequest(request, item);

            await UpdateInvoiceItem(request, item);
            if (request.IsApproved)
            {
                await InsertPaymentActivity(item);
            }

        }

        await unitOfWork.CompleteAsync();

    }

    #region Private Methods

    private async Task UpdateInvoiceCancelRequest(ApproveCancelInvoiceCommand request, InvoiceCancelRequest item)
    {
        item.Status = request.IsApproved ? InvoiceCancelStatus.Approved : InvoiceCancelStatus.Rejected;
        await _invoiceCancelRequestRepository.UpdateAsync(item);
    }

    private async Task UpdateInvoiceItem(ApproveCancelInvoiceCommand request, InvoiceCancelRequest item)
    {
        item.InvoiceItem.Status = request.IsApproved ? InvoiceItemStatus.Cancelled : InvoiceItemStatus.Unpaid;
        await _invoiceCancelRequestRepository.UpdateAsync(item);
    }

    private async Task InsertPaymentActivity(InvoiceCancelRequest item)
    {
        var paymentLog = new PaymentActivityLog
        {
            TenantId = item.TenantId,
            PatientId = item.PatientId,
            InvoiceId = item.InvoiceId,
            InvoiceItemId = item.InvoiceItemId,
            TransactionType = TransactionType.Other,
            TransactionAction = TransactionAction.CancelledInvoiceItem,
            InvoiceNo = item.Invoice.InvoiceId,
            FacilityId = item.FacilityId,
            Narration = "Cancelled Invoice",
        };
        await _paymentActivityLogRepository.InsertAsync(paymentLog);
    }

    #endregion

}