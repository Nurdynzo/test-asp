using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetPatientBloodTransfusionHistoryResponseDto : EntityDto<long>
    {
        public long PatientId { get; set; }
        public int PeriodSinceLastTransfusion { get; set; }
        public UnitOfTime Interval { get; set; }
        public int NumberOfPints { get; set; }
        public bool NoComplications { get; set; }
        public string Note { get; set; }
    }
}
