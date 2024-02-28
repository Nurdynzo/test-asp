using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class CreateCigaretteHistoryRequestDto
    {
        [Required]
        public long PatientId { get; set; }
        public bool PatientDoesNotConsumeTobacco { get; set; }
        public string FormOfTobacco { get; set; }
        public string Route { get; set; }
        public int NumberOfDaysPerWeek { get; set; }
        public int NumberOfPacksOrUnitsPerDay { get; set; }
        public bool StillTakesSubstance { get; set; }
        public string Note { get; set; }
        public int BeginningFrequency { get; set; }
        public UnitOfTime BeginningInterval { get; set; }
        public int EndFrequency { get; set; }
        public UnitOfTime EndInterval { get; set; }
        public long? Id { get; set; }
    }
}