using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class PatientFamilyMembersDto : EntityDto<long?>
    {
        public Relationship Relationship { get; set; }
        public bool IsAlive { get; set; }
        public int AgeAtDeath { get; set; }
        public string CausesOfDeath { get; set; }
        public string SeriousIllnesses { get; set; }
        public int AgeAtDiagnosis { get; set; }
    }
}
