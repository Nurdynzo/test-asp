using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientProductivePlans")]
    public class PatientProductivePlan : FullAuditedEntity<long>, IMustHaveTenant
    {
        public bool DoesPatientPlanToGetPregnantSoon { get; set; }
        
        public int TenantId { get; set; }
        
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}