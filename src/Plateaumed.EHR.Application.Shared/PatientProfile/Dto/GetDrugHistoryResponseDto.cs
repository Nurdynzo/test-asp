using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetDrugHistoryResponseDto : EntityDto<long>
    {
        public bool PatientOnMedication { get; set; }
        public long PatientId { get; set; }
        public string MedicationName { get; set; }
        public string Route { get; set; }
        public int Dose { get; set; }
        public string DoseUnit { get; set; }
        public int PrescriptionFrequency { get; set; }
        public string PrescriptionInterval { get; set; }
        public bool CompliantWithMedication { get; set; }
        public int UsageFrequency { get; set; }
        public string UsageInterval { get; set; }
        public bool IsMedicationStillBeingTaken { get; set; }
        public int WhenMedicationStopped { get; set; }
        public string StopInterval { get; set; }
        public string Note { get; set; }
    }
}
