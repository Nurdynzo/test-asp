using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

public interface IGetPaymentExpandableQueryHandler: ITransientDependency
{
    Task<PagedResultDto<GetPaymentExpandableQueryResponse>> Handle(GetPaymentExpandableQueryRequest request,long facilityId);
}