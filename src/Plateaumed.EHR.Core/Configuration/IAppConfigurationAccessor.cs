using Microsoft.Extensions.Configuration;

namespace Plateaumed.EHR.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
