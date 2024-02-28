using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Users.Dto;

namespace Plateaumed.EHR.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<PagedResultDto<UserLoginAttemptDto>> GetUserLoginAttempts(GetLoginAttemptsInput input);
    }
}
