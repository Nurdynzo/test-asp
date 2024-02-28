using Abp.Application.Services;
using Plateaumed.EHR.Dto;
using Plateaumed.EHR.Logging.Dto;

namespace Plateaumed.EHR.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
