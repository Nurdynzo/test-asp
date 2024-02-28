using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Plateaumed.EHR.ApiClient;
using Plateaumed.EHR.Mobile.MAUI.Core.ApiClient;

namespace Plateaumed.EHR
{
    [DependsOn(typeof(EHRClientModule), typeof(AbpAutoMapperModule))]

    public class EHRMobileMAUIModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            Configuration.ReplaceService<IApplicationContext, MAUIApplicationContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRMobileMAUIModule).GetAssembly());
        }
    }
}