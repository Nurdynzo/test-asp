using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

/// <summary>
/// Get invoice items for pricing
/// </summary>
public interface IGetInvoiceItemsForPricingQuery : ITransientDependency
{
    /// <summary>
    /// Query invoice items for pricing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="facilityId"></param>
    /// <returns></returns>
    Task<PagedResultDto<GetInvoiceItemPricingResponse>> Handle(GetInvoiceItemPricingRequest request, long facilityId);
}
