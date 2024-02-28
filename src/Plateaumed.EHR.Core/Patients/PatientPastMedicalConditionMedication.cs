using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.Patients
{
    [Table("PatientPastMedicalConditionMedications")]
    public class PatientPastMedicalConditionMedication: FullAuditedEntity<long>,IMustHaveTenant
    {

        public int TenantId { get; set; }
        public string MedicationType { get; set; }
        public string MedicationDose { get; set; }
        public int PrescriptionFrequency { get; set; }
        [StringLength(GeneralStringLengthConstant.ShortStringLength)]
        public string FrequencyUnit { get; set; }
        public bool IsCompliantWithMedication { get; set; }
        public int MedicationUsageFrequency { get; set; }
        [StringLength(GeneralStringLengthConstant.ShortStringLength)]
        public string MedicationUsageFrequencyUnit { get; set; }
        public long? PatientPastMedicalConditionId { get; set; }
        [ForeignKey("PatientPastMedicalConditionId")]
        public PatientPastMedicalCondition PatientPastMedicalCondition { get; set; }
    }
}