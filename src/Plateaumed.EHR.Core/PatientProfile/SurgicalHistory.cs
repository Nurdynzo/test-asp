using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("SurgicalHistories")]
    public class SurgicalHistory : FullAuditedEntity<long>
    {
        [Required]
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }

        public string Diagnosis { get; set; }

        public long? DiagnosisSnomedId { get; set; }

        public string Procedure { get; set; }

        public long? ProcedureSnomedId { get; set; } 

        public UnitOfTime Interval { get; set; }

        public int PeriodSinceSurgery { get; set; }

        public bool NoComplicationsPresent { get; set; }

        public string Note { get; set; }
    }
}
