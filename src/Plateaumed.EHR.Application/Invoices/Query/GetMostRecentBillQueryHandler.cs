using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Query;

/// <summary>
/// Get Most recent bill
/// </summary>
public class GetMostRecentBillQueryHandler : IGetMostRecentBillQueryHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
    private readonly IRepository<User , long> _userRepository;

    /// <summary>
    /// Constructor for GetMostRecentBillQueryHandler
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="invoiceItemRepository"></param>
    /// <param name="invoiceRepository"></param>
    public GetMostRecentBillQueryHandler(IRepository<User, long> userRepository, 
        IRepository<InvoiceItem, long> invoiceItemRepository,
        IRepository<Invoice, long> invoiceRepository)
    {
        _userRepository = userRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _invoiceRepository = invoiceRepository;
    }
    
    /// <summary>
    /// Get Most recent bill Handler
    /// </summary>
    /// <param name="patientId"></param>
    /// <returns></returns>

    public async Task<GetMostRecentBillResponse> Handle(long patientId)
    {
        var query = (from i in _invoiceRepository.GetAll().AsNoTracking()
                join u in _userRepository.GetAll() on i.CreatorUserId equals u.Id
                where i.PatientId == patientId
                orderby i.CreationTime descending
                select new GetMostRecentBillResponse
                {
                    Id = i.Id,
                    IssuedOn = i.CreationTime,
                    PaymentStatus = i.PaymentStatus.ToString(),
                    IssuedBy = u.FullName,
                    TotalAmount = i.TotalAmount.ToMoneyDto(),
                    Items = _invoiceItemRepository.GetAll().Where(x => x.InvoiceId == i.Id)
                        .Select(x => new MostRecentBillItems
                        {
                            Name = x.Name,
                            Quantity = x.Quantity,
                            SubTotal = x.UnitPrice.ToMoneyDto(),

                        }).ToList()
                }

            );
        return await query.FirstOrDefaultAsync();
    }
}
