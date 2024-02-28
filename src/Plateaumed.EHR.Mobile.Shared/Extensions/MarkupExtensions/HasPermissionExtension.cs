using System;
using Plateaumed.EHR.Core;
using Plateaumed.EHR.Core.Dependency;
using Plateaumed.EHR.Services.Permission;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plateaumed.EHR.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class HasPermissionExtension : IMarkupExtension
    {
        public string Text { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return false;
            }

            var permissionService = DependencyResolver.Resolve<IPermissionService>();
            return permissionService.HasPermission(Text);
        }
    }
}