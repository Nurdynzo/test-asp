using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Plateaumed.EHR
{
    [DependsOn(typeof(EHRXamarinSharedModule))]
    public class EHRXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRXamarinAndroidModule).GetAssembly());
        }
    }
}