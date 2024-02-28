using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Diagnoses
{
    [Table("Diagnosis")]
    [Audited]
    public class Diagnosis : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get ; set ; }
        public long PatientId { get; set; }
        
        public long Sctid { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int? Status { get; set; }

        public long? EncounterId { get; set; }

        [ForeignKey("EncounterId")]
        public PatientEncounter PatientEncounter { get; set; }
    }
}
