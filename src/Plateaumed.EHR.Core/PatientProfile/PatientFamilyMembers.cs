using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientFamilyMembers")]
    public class PatientFamilyMembers : FullAuditedEntity<long>
    {
        public Relationship Relationship { get; set; }
        public bool IsAlive { get; set; }
        public long PatientId { get; set; }
        public int AgeAtDeath { get; set; }
        public string CausesOfDeath { get; set; }
        public string SeriousIllnesses { get; set; }
        public int AgeAtDiagnosis { get; set; }
        public long? PatientFamilyHistoryId { get; set; }
        [ForeignKey("PatientFamilyHistoryId")]
        public PatientFamilyHistory PatientFamilyHistory { get; set; }
    }
}
