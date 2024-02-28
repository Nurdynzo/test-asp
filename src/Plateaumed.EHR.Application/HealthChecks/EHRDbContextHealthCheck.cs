using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Plateaumed.EHR.EntityFrameworkCore;

namespace Plateaumed.EHR.HealthChecks
{
    public class EHRDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public EHRDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("EHRDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("EHRDbContext could not connect to database"));
        }
    }
}
