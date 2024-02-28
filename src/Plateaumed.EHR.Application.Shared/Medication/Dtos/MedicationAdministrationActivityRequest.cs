using System.ComponentModel.DataAnnotations;
namespace Plateaumed.EHR.Medication.Dtos
{
    public class MedicationAdministrationActivityRequest
    {
        [Required]
        public long MedicationId { get; set; }
        [Required]
        public long PatientEncounterId { get; set; }
        public bool IsAvailable { get; set; }
        public string Direction { get; set; }
        public string Note { get; set; }
        public string DoseUnit { get; set; }
        public int? DoseValue { get; set; }
        public string FrequencyUnit { get; set; }
        public int? FrequencyValue { get; set; }
        public string DurationUnit { get; set; }
        public int? DurationValue { get; set; }
        public string ProductName { get; set; }
    }
}
