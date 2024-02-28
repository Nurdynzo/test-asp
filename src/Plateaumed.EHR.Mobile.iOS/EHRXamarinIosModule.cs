using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Plateaumed.EHR
{
    [DependsOn(typeof(EHRXamarinSharedModule))]
    public class EHRXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRXamarinIosModule).GetAssembly());
        }
    }
}