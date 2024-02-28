using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Plateaumed.EHR.Configure;
using Plateaumed.EHR.Startup;
using Plateaumed.EHR.Test.Base;

namespace Plateaumed.EHR.GraphQL.Tests
{
    [DependsOn(
        typeof(EHRGraphQLModule),
        typeof(EHRTestBaseModule))]
    public class EHRGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EHRGraphQLTestModule).GetAssembly());
        }
    }
}