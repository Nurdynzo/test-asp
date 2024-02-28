using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.Investigations
{
    [Table("InvestigationRequests")]
    [Audited]
    public class InvestigationRequest : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        
        public long InvestigationId { get; set; }

        [ForeignKey("InvestigationId")]
        public Investigation Investigation { get; set; }

        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        
        [ForeignKey("PatientEncounterId")]
        public PatientEncounter PatientEncounter { get; set; }

        public long? PatientEncounterId { get; set; }

        public bool Urgent { get; set; }

        public bool WithContrast { get; set; }

        public string Notes { get; set; }

        public long? DiagnosisId { get; set; }

        [ForeignKey("DiagnosisId")]
        public Diagnoses.Diagnosis Diagnosis { get; set; }

        public InvestigationStatus? InvestigationStatus { get; set; }
        
        public long? ProcedureId { get; set; }
    
        [ForeignKey("ProcedureId")]
        public virtual Procedure Procedure { get; set; }
    }
}
