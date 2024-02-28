using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientGynaecologicalIllness")]
    public class PatientGynaecologicalIllness : FullAuditedEntity<long>, IMustHaveTenant
    {
        public bool DoesPatientHaveSeriousGynaecologicalIllnessHistory { get; set; }

        public bool DoesPatientHaveGynaecologicalSurgicalHistory { get; set; }
        
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Diagnosis { get; set; }
        
        public long? DiagnosisSnomedId { get; set; }

        public int DiagnosisPeriod { get; set; }

        public UnitOfTime DiagnosisPeriodUnit { get; set; }

        public  ConditionControl ConditionControl { get; set; }

        public bool IsCurrentlyOnMedication { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string TypeOfMedication { get; set; }

        public int Dosage { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string DosageUnit { get; set; }

        public int PrescriptionFrequency { get; set; }
        
        public UnitOfTime PrescriptionFrequencyUnit { get; set; }

        public bool IsComplaintWithMedication { get; set; }

        public int MedicationUsageFrequency { get; set; }

        public UnitOfTime MedicationUsageFrequencyUnit { get; set; }

        public string Notes { get; set; }
        
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        
        public int TenantId { get; set; }
    }
}