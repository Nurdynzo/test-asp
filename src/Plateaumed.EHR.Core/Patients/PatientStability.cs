using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Patients
{
    public class PatientStability : FullAuditedEntity<long>, IMustHaveTenant
    {
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }

        public int TenantId { get; set; }

        public long EncounterId { get; set; }

        [ForeignKey("EncounterId")]
        public PatientEncounter PatientEncounterFk { get; set; }

        public PatientStabilityStatus Status { get; set; }
    }
}

