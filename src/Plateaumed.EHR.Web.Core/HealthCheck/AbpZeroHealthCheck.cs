using Microsoft.Extensions.DependencyInjection;
using Plateaumed.EHR.HealthChecks;

namespace Plateaumed.EHR.Web.HealthCheck
{
    public static class AbpZeroHealthCheck
    {
        public static IHealthChecksBuilder AddAbpZeroHealthCheck(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();
            builder.AddCheck<EHRDbContextHealthCheck>("Database Connection");
            builder.AddCheck<EHRDbContextUsersHealthCheck>("Database Connection with user check");
            builder.AddCheck<CacheHealthCheck>("Cache");

            // add your custom health checks here
            // builder.AddCheck<MyCustomHealthCheck>("my health check");

            return builder;
        }
    }
}
