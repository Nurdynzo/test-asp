using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

/// <summary>
/// Get Payment Summary Query
/// </summary>
public interface IGetPaymentSummaryQueryHandler: ITransientDependency
{
    /// <summary>
    /// Handler for GetPaymentSummaryQuery
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PagedResultDto<GetPaymentSummaryQueryResponse>> Handle(GetPaymentSummaryQueryRequest request);
}