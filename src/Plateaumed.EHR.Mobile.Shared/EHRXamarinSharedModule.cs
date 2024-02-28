using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Plateaumed.EHR
{
    [DependsOn(typeof(EHRClientModule), typeof(AbpAutoMapperModule))]
    public class EHRXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRXamarinSharedModule).GetAssembly());
        }
    }
}