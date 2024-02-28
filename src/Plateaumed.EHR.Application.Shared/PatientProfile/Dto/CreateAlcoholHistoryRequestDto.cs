using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class CreateAlcoholHistoryRequestDto
    {
        [Required]
        public long PatientId { get; set; }
        public int Frequency { get; set; }
        public UnitOfTime Interval { get; set; }
        public string TypeOfAlcohol { get; set; }
        public float MaximumAmountOfUnits { get; set; }
        public string MaximumUnitTaken { get; set; }
        public string Note { get; set; }
        public bool DetailsOfAlcoholIntakeNotKnown { get; set; }
        public bool DoesNotTakeAlcohol { get; set; }
        public long? Id { get; set; }
    }
}
