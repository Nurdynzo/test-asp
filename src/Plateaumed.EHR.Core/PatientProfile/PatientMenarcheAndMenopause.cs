using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientMenarcheAndMenopause")]
    public class PatientMenarcheAndMenopause : FullAuditedEntity<long>, IMustHaveTenant
    {
        public bool IsMenarcheKnown { get; set; }

        public bool IsMenopauseKnown { get; set; }

        public int MenarcheAge { get; set; }

        public int MenopauseAge { get; set; }
        
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string PostMenopausalSymptoms { get; set; }

        public long? PostMenopausalSymptomsSnomedId { get; set; }
        
        public int TenantId { get; set; }
        
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}