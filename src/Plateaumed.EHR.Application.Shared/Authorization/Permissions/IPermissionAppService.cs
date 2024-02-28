using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Permissions.Dto;

namespace Plateaumed.EHR.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
