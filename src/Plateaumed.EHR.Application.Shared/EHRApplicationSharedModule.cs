using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Plateaumed.EHR
{
    [DependsOn(typeof(EHRCoreSharedModule))]
    public class EHRApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRApplicationSharedModule).GetAssembly());
        }
    }
}