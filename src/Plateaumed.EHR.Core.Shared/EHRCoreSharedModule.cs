using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Plateaumed.EHR
{
    public class EHRCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRCoreSharedModule).GetAssembly());
        }
    }
}