using Abp.AspNetCore.Mvc.ViewComponents;

namespace Plateaumed.EHR.Web.Public.Views
{
    public abstract class EHRViewComponent : AbpViewComponent
    {
        protected EHRViewComponent()
        {
            LocalizationSourceName = EHRConsts.LocalizationSourceName;
        }
    }
}