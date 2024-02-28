using System;
using System.Collections.Generic;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class TreatmentMedication
    {
        public DateTime Date { get; set; }
        public string Medication { get; set; }
        public string DosageAndUnit { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public string DoseAdministered { get; set; }
        public List<TreatmentMedicationDetails> Details { get; set; }
    }
}