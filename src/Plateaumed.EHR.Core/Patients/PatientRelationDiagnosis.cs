using Abp.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Patients
{
    [Table("PatientRelationsDiagnoses")]
    public class PatientRelationDiagnosis : Entity<long>
    {
        [StringLength(PatientRelationDiagnosisConsts.MaxSctIdLength, MinimumLength = PatientRelationDiagnosisConsts.MinSctIdLength)]
        public string SctId { get; set; }

        [StringLength(PatientRelationDiagnosisConsts.MaxDiagnosisLength, MinimumLength = PatientRelationDiagnosisConsts.MinDiagnosisLength)]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsCauseOfDeath { get; set; } = false;

        public virtual long PatientRelationId { get; set; }

        [ForeignKey("PatientRelationId")]
        public PatientRelation PatientRelation { get; set; }
    }
}
