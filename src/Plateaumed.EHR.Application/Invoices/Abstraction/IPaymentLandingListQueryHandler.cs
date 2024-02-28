using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

public interface IPaymentLandingListQueryHandler: ITransientDependency
{
    Task<PagedResultDto<GetPaymentLadingListQueryResponse>> Handle(
        PaymentLandingListFilterRequest request,
        long facilityId);
}