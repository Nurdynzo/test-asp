using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class SaveMenstrualBloodFlowCommandRequest: EntityDto<long?>
    {
        public bool IsPeriodHeavierThanUsual { get; set; }
        
        public bool IsBloodClotLargerThanRegular { get; set; }

        public int  SanitaryPadUsedPerDay { get; set; }

        public bool IsHeavyPeriodImpactDayToDayLife { get; set; }

        public bool IsFlowFloodThroughSanitaryTowel { get; set; }

        public MenstrualFlowType FlowType { get; set; }
        [Required]
        public long PatientId { get; set; }
    }
}
