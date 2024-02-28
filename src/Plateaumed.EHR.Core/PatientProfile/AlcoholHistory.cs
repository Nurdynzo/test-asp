using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("AlcoholHistories")]
    public class AlcoholHistory : FullAuditedEntity<long>
    {
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }
        public int Frequency { get; set; }
        public UnitOfTime Interval { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string TypeOfAlcohol { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string MaximumUnitTaken { get; set; }
        public float MaximumAmountOfUnits { get; set; }
        public string Note { get; set; }
        public bool DetailsOfAlcoholIntakeNotKnown { get; set; }
        public bool DoesNotTakeAlcohol { get; set; }
    }
}
