using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class CreateSurgicalHistoryRequestDto
    {
        [Required]
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
