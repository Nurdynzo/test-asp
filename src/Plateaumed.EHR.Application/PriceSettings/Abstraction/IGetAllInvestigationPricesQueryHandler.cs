using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.PriceSettings.Dto;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PriceSettings.Abstraction
{
	public interface IGetInvestigationPricesQueryHandler : ITransientDependency
    {
        Task<PagedResultDto<GetInvestigationPricingResponseDto>> Handle(GetInvestigationPricingRequestDto request);

    }
}

