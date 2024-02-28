using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Command;

public class ConvertProformaIntoInvoiceCommandHandler : IConvertProformaIntoInvoiceCommandHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IUnitOfWorkManager _unitOfWork;
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepository;
    public ConvertProformaIntoInvoiceCommandHandler(IRepository<Invoice, long> invoiceRepository,
        IUnitOfWorkManager unitOfWork, 
        IRepository<PaymentActivityLog, long> paymentActivityLogRepository)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
        _paymentActivityLogRepository = paymentActivityLogRepository;
    }

    public async Task Handle(ProformaToNewInvoiceRequest request, long facilityId)
    {
        var invoice = await _invoiceRepository
            .GetAll()
            .AsNoTracking()
            .Include(x => x.InvoiceItems)
            .FirstOrDefaultAsync(x => x.PatientId == request.PatientId && x.FacilityId == facilityId &&
                                      x.InvoiceType == InvoiceType.Proforma) ?? throw new UserFriendlyException("Invoice not found");
        
       
        using var uow = _unitOfWork.Begin();
        invoice.FacilityId = facilityId;
        invoice.InvoiceType = InvoiceType.InvoiceCreation;
        invoice.InvoiceId = request.InvoiceNo;
        invoice.TotalAmount = request.TotalAmount.ToMoney();
        ValidateIfAmountIsCalculatedCorrectly(invoice);
        await _invoiceRepository.UpdateAsync(invoice);
        foreach (var x in invoice.InvoiceItems)
        {
           await InsertPaymentActivity(x, x.TenantId.GetValueOrDefault(), invoice.PatientId, invoice.Id, invoice.InvoiceId, facilityId);
        }


        await uow.CompleteAsync();



    }
    
    private static void ValidateIfAmountIsCalculatedCorrectly(Invoice request)
    {
        if (request.InvoiceItems.Count == 0)
        {
            throw new UserFriendlyException("Invoice items cannot be empty");
        }

        var amountAfterDiscountApplied = request.InvoiceItems.Sum(x => x.DiscountPercentage > 0 ?
            (x.UnitPrice.Amount * x.Quantity) - ((x.UnitPrice.Amount * x.Quantity) * x.DiscountPercentage / 100) : x.UnitPrice.Amount * x.Quantity);


        if (Math.Round(amountAfterDiscountApplied.GetValueOrDefault(), 2) != request.TotalAmount.Amount)
        {
            throw new UserFriendlyException("Invoice amount is not calculated correctly");
        }
    }
    private async Task InsertPaymentActivity(InvoiceItem item, 
        int tenantId, 
        long patientId,
        long invoiceId,
        string invoiceNo,
        long facilityId)
    {
        
            var paymentLog = new PaymentActivityLog
            {
                TenantId = tenantId,
                PatientId = patientId,
                InvoiceId = invoiceId,
                InvoiceItemId = item.Id,
                TransactionType = TransactionType.Other,
                TransactionAction = TransactionAction.CreateInvoice,
                InvoiceNo = invoiceNo,
                FacilityId = facilityId,
                Narration = "Convert proforma to new invoice",
                ActualAmount = item.UnitPrice
                
            };
            await _paymentActivityLogRepository.InsertAsync(paymentLog);
        
    }
}