using Abp.AspNetCore.Mvc.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Storage;
using Abp.BackgroundJobs;

namespace Plateaumed.EHR.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}