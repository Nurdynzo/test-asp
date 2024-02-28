using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientPastMedicalConditionMedicationResponse: EntityDto<long>
    {
        public string MedicationType { get; set; }
        public string MedicationDose { get; set; }
        public string PrescriptionFrequency { get; set; }
        public string FrequencyUnit { get; set; }
        public bool IsCompliantWithMedication { get; set; }
        public string MedicationUsageFrequency { get; set; }
        public string MedicationUsageFrequencyUnit { get; set; }
    }
}
