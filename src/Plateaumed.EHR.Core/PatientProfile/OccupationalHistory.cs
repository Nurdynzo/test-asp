using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("OccupationalHistories")]
    public class OccupationalHistory : FullAuditedEntity<long>
    {
        public string WorkLocation { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Occupation { get; set; }
        public string Note { get; set; }
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }
        public bool IsCurrent { get; set; }
    }
}
