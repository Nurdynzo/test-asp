using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Query;

public class GetAllProformaInvoiceQueryHandler : IGetAllProformaInvoiceQueryHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    
    public GetAllProformaInvoiceQueryHandler(
        IRepository<Invoice, long> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<GetAllProformaInvoiceQueryResponse> Handle(long patientId, long facilityId)
    {
        var query = await _invoiceRepository
            .GetAll()
            .AsNoTracking()
            .Include(x => x.InvoiceItems)
            .Where(x => x.PatientId == patientId && x.FacilityId == facilityId && x.InvoiceType == InvoiceType.Proforma)
            .Select(i => new GetAllProformaInvoiceQueryResponse
            {
                Id = i.Id,
                InvoiceNo = i.InvoiceId,
                CreatedDate = i.CreationTime,
                InvoiceItems = i.InvoiceItems
                    .Where(x => x.Status == InvoiceItemStatus.Unpaid)
                    .Select(x => new InvoiceItemResponse
                    {
                        Id = x.Id,
                        SubTotal = x.SubTotal != null
                            ? new MoneyDto
                            {
                                Amount = x.SubTotal.Amount,
                                Currency = x.SubTotal.Currency
                            }
                            : null,
                        Name = x.Name,
                        Notes = x.Notes,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice != null
                            ? new MoneyDto
                            {
                                Amount = x.UnitPrice.Amount,
                                Currency = x.UnitPrice.Currency
                            }: null,
                        DiscountAmount = x.DiscountAmount != null
                            ? new MoneyDto
                            {
                                Amount = x.DiscountAmount.Amount,
                                Currency = x.DiscountAmount.Currency
                            }
                            : null,
                        DiscountPercentage = x.DiscountPercentage,

                    })
                    .ToList()
            }).ToListAsync();
        return query.FirstOrDefault();
    }

}