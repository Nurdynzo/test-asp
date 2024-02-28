using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientMenstrualFlows")] 
    public class PatientMenstrualFlow : FullAuditedEntity<long>, IMustHaveTenant
    {
        public bool IsPeriodHeavierThanUsual { get; set; }
        
        public bool IsBloodClotLargerThanRegular { get; set; }

        public int  SanitaryPadUsedPerDay { get; set; }

        public bool IsHeavyPeriodImpactDayToDayLife { get; set; }

        public bool IsFlowFloodThroughSanitaryTowel { get; set; }

        public MenstrualFlowType FlowType { get; set; }
        
        public int TenantId { get; set; }
        
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}