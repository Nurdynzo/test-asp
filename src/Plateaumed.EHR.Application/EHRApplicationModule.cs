using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Plateaumed.EHR.Authorization;

namespace Plateaumed.EHR
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(EHRApplicationSharedModule),
        typeof(EHRCoreModule)
        )]
    public class EHRApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRApplicationModule).GetAssembly());
        }
    }
}