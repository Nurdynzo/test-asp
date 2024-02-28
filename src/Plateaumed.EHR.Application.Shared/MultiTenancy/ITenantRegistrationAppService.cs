using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Editions.Dto;
using Plateaumed.EHR.MultiTenancy.Dto;

namespace Plateaumed.EHR.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<SuggestTenantCountryByIpOutput> SuggestTenantCountryByIp();

        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<GetTenantOnboardingProgressOutput> GetTenantOnboardingProgress();

        Task UpdateTenantOnboardingProgress(UpdateTenantOnboardingProgressInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}