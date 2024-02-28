using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetSurgicalHistoryResponseDto : EntityDto<long>
    {
        public long PatientId { get; set; }
        public string Diagnosis { get; set; }
        public long? DiagnosisSnomedId { get; set; }
        public string Procedure { get; set; }
        public long? ProcedureSnomedId { get; set; }
        public UnitOfTime Interval { get; set; }
        public int PeriodSinceSurgery { get; set; }
        public bool NoComplicationsPresent { get; set; }
        public string Note { get; set; }
    }
}
