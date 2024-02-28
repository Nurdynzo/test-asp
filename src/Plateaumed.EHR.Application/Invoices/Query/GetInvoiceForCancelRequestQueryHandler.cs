using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.Invoices.Query;

public class GetInvoiceForCancelRequestQueryHandler : IGetInvoiceForCancelRequestQueryHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;

    public GetInvoiceForCancelRequestQueryHandler(IRepository<Invoice, long> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<List<GetInvoiceForCancelQueryResponse>> Handle(long patientId, long facilityId)
    {
        var query = await _invoiceRepository
            .GetAll()
            .AsNoTracking()
            .Include(x=>x.InvoiceItems)
            .Where(x => x.PatientId == patientId && x.FacilityId == facilityId )
            .Select(i => new GetInvoiceForCancelQueryResponse
            {
                Id = i.Id,
                InvoiceNo = i.InvoiceId,
                CreatedDate = i.CreationTime,
                InvoiceItems = i.InvoiceItems.Where(x=>x.Status == InvoiceItemStatus.Unpaid)
                    .Select(x => new CancelInvoiceItemsQueryResponse
                {
                    Id = x.Id,
                   SubTotal = x.SubTotal != null ? new MoneyDto
                   {
                       Amount = x.SubTotal.Amount,
                       Currency = x.SubTotal.Currency
                   }:null,
                   ItemName = x.Name
                }).ToArray()
                
            }
            ).ToListAsync();
        return query.ToList();
    }
}