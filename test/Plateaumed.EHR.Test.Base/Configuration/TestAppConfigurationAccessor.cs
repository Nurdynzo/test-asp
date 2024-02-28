using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Plateaumed.EHR.Configuration;

namespace Plateaumed.EHR.Test.Base.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(EHRTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
