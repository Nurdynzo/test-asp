using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("BloodTransfusionHistories")]
    public class BloodTransfusionHistory : FullAuditedEntity<long>
    {
        [Required]
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }

        public int PeriodSinceLastTransfusion { get; set; }

        public UnitOfTime Interval { get; set; }

        public int NumberOfPints { get; set; }

        public bool NoComplications { get; set; }

        public string Note { get; set; }
    }
}
