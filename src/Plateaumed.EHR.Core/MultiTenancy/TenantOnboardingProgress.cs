using Abp.Domain.Entities;
using Abp.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.MultiTenancy
{
    [Table("TenantOnboardingProgress")]
    [Audited]
    public class TenantOnboardingProgress : Entity<long>, IMustHaveTenant
    {
        public TenantOnboardingStatus TenantOnboardingStatus { get; set; }

        public bool Completed { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        public int TenantId { get; set; }
    }
}
