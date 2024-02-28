using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.PatientWallet.Query;

public class GetListOfInvoiceItemsForRefundHandler : IGetListOfInvoiceItemsForRefundHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepository;

    public GetListOfInvoiceItemsForRefundHandler(IRepository<Invoice, long> invoiceRepository,
        IRepository<InvoiceItem, long> invoiceItemRepository)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
    }

    public async Task<List<RefundInvoiceQueryResponse>> Handle(RefundInvoiceQueryRequest request, long facilityId)
    {
        if (request.InvoiceIds?.Length == 0)
        {
            throw new UserFriendlyException("Please select at least one invoice");
        }
        var query = from i in _invoiceRepository.GetAll()
                    join ii in _invoiceItemRepository.GetAll() on i.Id equals ii.InvoiceId
                    where request.InvoiceIds.Contains(i.Id) 
                          && i.PatientId == request.PatientId
                          && ii.Status == InvoiceItemStatus.Paid
                          && ii.FacilityId == facilityId
                    orderby i.TimeOfInvoicePaid descending 
                    group ii by new {  i.Id, i.TimeOfInvoicePaid, i.InvoiceId } into g
                    select new RefundInvoiceQueryResponse
                    {
                     Id = g.Key.Id,
                     InvoiceNo = g.Key.InvoiceId,
                     PaymentDate = g.Key.TimeOfInvoicePaid,
                     InvoiceItems = g.Select(x => new RefundInvoiceItemsQueryResponse
                     {
                         Id = x.Id,
                         ItemName = x.Name,
                         SubTotal = new MoneyDto{ Amount = x.SubTotal.Amount, Currency = x.SubTotal.Currency }
                         
                     }).ToArray()
                    };
        query = request switch
        {
            { DateFilter: WalletRefundFilter.Today } => query.Where(x => x.PaymentDate == DateTime.Today),
            { DateFilter: WalletRefundFilter.Yesterday } => query.Where(x => x.PaymentDate >= DateTime.Today.AddDays(-1)),
            { DateFilter: WalletRefundFilter.ThisWeek } => query.Where(x => x.PaymentDate >= DateTime.Today.AddDays(-7)),
            { DateFilter: WalletRefundFilter.LastWeek } => query.Where(x => x.PaymentDate >= DateTime.Today.AddDays(-14)),
            { DateFilter: WalletRefundFilter.ThisMonth } => query.Where(x => x.PaymentDate >= DateTime.Today.AddDays(-30)),
            { DateFilter: WalletRefundFilter.LastMonth } => query.Where(x => x.PaymentDate >= DateTime.Today.AddDays(-60)),
            { DateFilter: WalletRefundFilter.ThisYear } => query.Where(x => x.PaymentDate >= DateTime.Today.AddDays(-365)),
            { DateFilter: WalletRefundFilter.LastYear } => query.Where(x => x.PaymentDate >= DateTime.Today.AddDays(-730)),
            { CustomDate: not null } => query.Where(x => x.PaymentDate >= request.CustomDate),
            _ => query
        };
        return await query.ToListAsync();
                    
    }
}