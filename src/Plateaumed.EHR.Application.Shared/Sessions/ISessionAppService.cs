using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Sessions.Dto;

namespace Plateaumed.EHR.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
