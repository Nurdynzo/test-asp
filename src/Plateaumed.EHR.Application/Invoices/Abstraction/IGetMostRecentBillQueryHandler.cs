using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

/// <summary>
/// Interface for GetInvoiceItemsForPricingQuery
/// </summary>
public interface IGetMostRecentBillQueryHandler : ITransientDependency
{
    /// <summary>
    /// Get Most recent bill Handler
    /// </summary>
    /// <param name="patientId"></param>
    /// <returns></returns>
    Task<GetMostRecentBillResponse> Handle(long patientId);
}