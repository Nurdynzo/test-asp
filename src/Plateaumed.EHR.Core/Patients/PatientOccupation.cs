using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using System;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Patients
{
    [Table("PatientOccupations")]
    [Audited]
    public class PatientOccupation : FullAuditedEntity<long>
    {

        public virtual long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } 

        public virtual long OccupationId { get; set; }

        [ForeignKey("OccupationId")]
        public Occupation Occupation { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual bool IsCurrent { get; set; }

        [StringLength(
            PatientOccupationConsts.MaxLocationOfWorkLength,
            MinimumLength = PatientOccupationConsts.MinLocationOfWorkLength
        )]
        public virtual string Location { get; set; }

        [StringLength(
            PatientOccupationConsts.MaxNotesLength,
            MinimumLength = PatientOccupationConsts.MinNotesLength
        )]
        public virtual string Notes { get; set; }
    }
}