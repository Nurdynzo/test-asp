using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Configuration.Tenants.Dto;

namespace Plateaumed.EHR.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
