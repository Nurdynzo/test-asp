using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("ReviewOfSystems")]
    public class ReviewOfSystem : FullAuditedEntity<long>
    {
        public string Name { get; set; }

        public long SnomedId { get; set; }

        public SymptomsCategory Category { get; set; }

        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }
    }
}
