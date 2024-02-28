using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientMenstrualPains")]
    public class PatientMenstrualPain : FullAuditedEntity<long>, IMustHaveTenant
    {
        public bool IsPeriodPainInterfereWithDayToDayLife { get; set; }

        public bool IsRecentPeriodMorePainfulThanUsual { get; set; }
        
        public int TenantId { get; set; }
        
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}