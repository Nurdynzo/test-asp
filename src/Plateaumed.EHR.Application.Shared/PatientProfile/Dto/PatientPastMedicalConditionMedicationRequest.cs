using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class PatientPastMedicalConditionMedicationRequest
    {
        public string MedicationType { get; set; }
        public string MedicationDose { get; set; }
        public int PrescriptionFrequency { get; set; }
        public string FrequencyUnit { get; set; }
        public bool IsCompliantWithMedication { get; set; }
        public int MedicationUsageFrequency { get; set; }
        public string MedicationUsageFrequencyUnit { get; set; }
        public long? Id { get; set; }
    }
}
