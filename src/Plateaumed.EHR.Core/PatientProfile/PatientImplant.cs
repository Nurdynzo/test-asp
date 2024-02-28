using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientImplants")]
    public class PatientImplant : FullAuditedEntity<long>
    { 
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }
        public string Name { get; set; }
        public long? SnomedId { get; set; }
        public bool IsIntact { get; set; }
        public bool HasComplications { get; set; }
        public string Note { get; set; }
        public DateTime DateInserted { get; set; }
        public DateTime DateRemoved { get; set; }
    }
}
