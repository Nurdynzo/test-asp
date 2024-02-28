using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("CigeretteAndTobaccoHistories")]
    public class CigeretteAndTobaccoHistory : FullAuditedEntity<long>
    {
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }
        public bool PatientDoesNotConsumeTobacco { get; set; }
        public string FormOfTobacco { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Route { get; set; }
        public int NumberOfDaysPerWeek { get; set; }
        public int NumberOfPacksOrUnitsPerDay { get; set; }
        public bool StillTakesSubstance { get; set; }
        public string Note { get; set; }
        public int BeginningFrequency { get; set; }
        public UnitOfTime BeginningInterval { get; set; }
        public int EndFrequency { get; set; }
        public UnitOfTime EndInterval { get; set; }
    }
}
