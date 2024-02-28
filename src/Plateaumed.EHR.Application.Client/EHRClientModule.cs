using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Plateaumed.EHR
{
    public class EHRClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRClientModule).GetAssembly());
        }
    }
}
