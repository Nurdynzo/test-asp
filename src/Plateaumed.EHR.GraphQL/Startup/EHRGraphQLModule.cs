using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Plateaumed.EHR.Startup
{
    [DependsOn(typeof(EHRCoreModule))]
    public class EHRGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}