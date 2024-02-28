using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Install.Dto;

namespace Plateaumed.EHR.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}