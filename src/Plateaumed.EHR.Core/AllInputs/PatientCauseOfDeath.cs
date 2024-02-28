using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.AllInputs
{
    public class PatientCauseOfDeath : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long DischargeId { get; set; }
        [ForeignKey("DischargeId")]
        public virtual Discharge PatientDischarge { get; set; }
        public string CausesOfDeath { get; set; }
    }
}
