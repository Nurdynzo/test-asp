using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Configuration.Host.Dto;

namespace Plateaumed.EHR.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
