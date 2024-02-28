using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Plateaumed.EHR.Web.Public.Views
{
    public abstract class EHRRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected EHRRazorPage()
        {
            LocalizationSourceName = EHRConsts.LocalizationSourceName;
        }
    }
}
