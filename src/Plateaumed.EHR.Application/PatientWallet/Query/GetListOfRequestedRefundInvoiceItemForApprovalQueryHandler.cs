using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.PatientWallet.Query;

public class GetListOfRequestedRefundInvoiceItemForApprovalQueryHandler : 
    IGetListOfRequestedRefundInvoiceItemForApprovalQueryHandler
{
    private readonly IRepository<InvoiceRefundRequest,long> _invoiceRefundRequestRepository;
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepository;

    public GetListOfRequestedRefundInvoiceItemForApprovalQueryHandler(
        IRepository<InvoiceRefundRequest, long> invoiceRefundRequestRepository, 
        IRepository<Invoice, long> invoiceRepository, 
        IRepository<InvoiceItem, long> invoiceItemRepository)
    {
        _invoiceRefundRequestRepository = invoiceRefundRequestRepository;
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
    }

    public async Task<List<RefundInvoiceQueryResponse>> Handle( long patientId, long facilityId)
    {
        var query = from ii in _invoiceRefundRequestRepository.GetAll()
            join i in _invoiceRepository.GetAll() on ii.InvoiceId equals i.Id
            join it in _invoiceItemRepository.GetAll() on ii.InvoiceItemId equals it.Id
            where ii.PatientId == patientId && ii.FacilityId == facilityId && ii.Status == InvoiceRefundStatus.Pending
            group new {ii,it} by new { ii.InvoiceId, i.TimeOfInvoicePaid, InvoiceNo = i.InvoiceId }
            into g
            select new RefundInvoiceQueryResponse
            {
                Id = g.First().ii.Id,
                InvoiceNo = g.Key.InvoiceNo,
                PaymentDate = g.Key.TimeOfInvoicePaid,
                PercentageToBeDeducted = Constants.RefundDeductionPercentage,
                InvoiceItems = g.Select(x => new RefundInvoiceItemsQueryResponse
                {
                    Id = x.it.Id,
                    ItemName = x.it.Name,
                    SubTotal = new MoneyDto { Amount = x.it.SubTotal.Amount, Currency = x.it.SubTotal.Currency }

                }).ToArray()
            };
        return await query.ToListAsync();

    }
}