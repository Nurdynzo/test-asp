using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.AllInputs
{
    public class MedicationAdministrationActivity: FullAuditedEntity<long>
    {
        public long MedicationId { get; set; }
        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }
        public long PatientEncounterId { get; set; }
        [ForeignKey("PatientEncounterId")]
        public PatientEncounter PatientEncounter { get; set; }
        public bool IsAvailable { get; set; }
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Direction { get; set; }
        public string Note { get; set; }
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string DoseUnit { get; set; }
        public int? DoseValue { get; set; }
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string FrequencyUnit { get; set; }
        public int? FrequencyValue { get; set; }
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string DurationUnit { get; set; }
        public int? DurationValue { get; set; }
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string ProductName { get; set; }
    }
}
