using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Query;

/// <summary>
/// Get invoice and invoice items
/// </summary>
public class GetInvoiceAndInvoiceItemsByIdQueryHandler : IGetInvoiceAndInvoiceItemsByIdQueryHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;

    /// <summary>
    /// constructor for GetInvoiceAndInvoiceItemsByIdQueryHandler
    /// </summary>
    /// <param name="invoiceRepository"></param>
    public GetInvoiceAndInvoiceItemsByIdQueryHandler(IRepository<Invoice, long> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }
    
    
    /// <summary>
    /// Handle get invoice and invoice items by patient id
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    public async Task<GetInvoiceQueryResponse> Handle(long invoiceId)
    {
        var query = await _invoiceRepository
            .GetAll()
            .Include(x=>x.InvoiceItems)
            .AsNoTracking()
            .Where(x=>x.Id == invoiceId)
            .Select(i => new GetInvoiceQueryResponse
            {
                Id = i.Id,
                InvoiceNo = i.InvoiceId,
                PatientId = i.PatientId,
                PaymentType = i.PaymentType.ToString(),
                TotalAmount = i.TotalAmount.ToMoneyDto(),
                IsServiceOnCredit = i.PaymentType == PaymentTypes.ServiceOnCredit,
                AppointmentId = i.PatientAppointmentId.GetValueOrDefault(),
                Items = i.InvoiceItems.Select(x => new InvoiceItemRequest()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice.ToMoneyDto(),
                    SubTotal = x.SubTotal.ToMoneyDto(),
                    DiscountPercentage = x.DiscountPercentage.GetValueOrDefault(),
                    IsGlobal = x.DiscountPercentage > 0,
                    IsDeleted = x.IsDeleted,


                })
                
            }).FirstOrDefaultAsync();
        return query;
    }
    
  
    
}
