using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.MultiTenancy.Abstractions;
using Plateaumed.EHR.MultiTenancy.Dto;

namespace Plateaumed.EHR.MultiTenancy.Handlers
{
    /// <inheritdoc />
    public class UpdateTenantOnboardingProgressCommandHandler : IUpdateTenantOnboardingProgressCommandHandler
    {
        private readonly ITenantManager _tenantManager;
        private readonly IAbpSession _abpSession;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tenantManager"></param>
        /// <param name="abpSession"></param>
        public UpdateTenantOnboardingProgressCommandHandler(ITenantManager tenantManager, IAbpSession abpSession)
        {
            _tenantManager = tenantManager;
            _abpSession = abpSession;
        }

        /// <inheritdoc />
        public async Task Handle(UpdateTenantOnboardingProgressInput request)
        {
            var tenant = await _tenantManager.GetByIdAsync(_abpSession.GetTenantId()) ??
                         throw new UserFriendlyException("Tenant not found");

            request.OnboardingProgress.ForEach(p =>
            {
                var tenantOnboardingProgress =
                    tenant.OnboardingProgress.FirstOrDefault(o => o.TenantOnboardingStatus == p.TenantOnboardingStatus);
                if (tenantOnboardingProgress != null)
                {
                    tenantOnboardingProgress.Completed = p.Completed;
                }
                else
                {
                    tenant.OnboardingProgress.Add(new TenantOnboardingProgress
                    {
                        TenantId = tenant.Id,
                        TenantOnboardingStatus = p.TenantOnboardingStatus,
                        Completed = p.Completed
                    });
                }

                if (p.TenantOnboardingStatus == TenantOnboardingStatus.Finalize)
                {
                    tenant.IsOnboarded = p.Completed;
                }
            });

            await _tenantManager.UpdateAsync(tenant);
        }
    }
}