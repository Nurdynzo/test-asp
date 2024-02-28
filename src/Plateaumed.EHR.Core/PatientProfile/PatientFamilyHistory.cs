using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientFamilyHistories")]
    public class PatientFamilyHistory: FullAuditedEntity<long>
    {
        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public bool IsFamilyHistoryKnown { get; set; }
        public int TotalNumberOfSiblings { get; set; }
        public int TotalNumberOfMaleSiblings { get; set; }
        public int TotalNumberOfFemaleSiblings { get; set; }
        public int TotalNumberOfChildren { get; set; }
        public int TotalNumberOfMaleChildren { get; set; }
        public int TotalNumberOfFemaleChildren { get; set; }
        public ICollection<PatientFamilyMembers> PatientFamilyMembers { get; set; }

    }

}
