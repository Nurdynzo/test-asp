using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Onboarding.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Onboarding
{
    public interface IOnboardingReviewsAppService : IApplicationService
    {
        Task<OnboardingReviewsDto> GetReviewDetails(GetAllFacilitiesInput input);
    }
}
