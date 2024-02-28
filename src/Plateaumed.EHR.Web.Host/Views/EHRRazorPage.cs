using Abp.AspNetCore.Mvc.Views;

namespace Plateaumed.EHR.Web.Views
{
    public abstract class EHRRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected EHRRazorPage()
        {
            LocalizationSourceName = EHRConsts.LocalizationSourceName;
        }
    }
}
