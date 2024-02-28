using Abp.AspNetCore.Mvc.Authorization;
using Plateaumed.EHR.Authorization.Users.Profile;
using Plateaumed.EHR.Graphics;
using Plateaumed.EHR.Storage;

namespace Plateaumed.EHR.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(
            ITempFileCacheManager tempFileCacheManager,
            IProfileAppService profileAppService,
            IImageFormatValidator imageFormatValidator) :
            base(tempFileCacheManager, profileAppService, imageFormatValidator)
        {
        }
    }
}