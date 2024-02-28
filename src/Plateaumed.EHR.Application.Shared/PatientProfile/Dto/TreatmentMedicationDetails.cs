using System;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class TreatmentMedicationDetails
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string DosageAndUnit { get; set; }
        public string DoseAdministered { get; set; }
        public string Notes { get; set; }
        public int DoseValue { get; set; }
    }
}
