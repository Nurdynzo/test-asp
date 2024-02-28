using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class CreateBloodTransfusionHistoryRequestDto
    {
        [Required]
        public long PatientId { get; set; }
        public int PeriodSinceLastTransfusion { get; set; }
        public UnitOfTime Interval { get; set; }

        [Required]
        public int NumberOfPints { get; set; }
        public bool NoComplications { get; set; }
        public string Note { get; set; }
    }
}
