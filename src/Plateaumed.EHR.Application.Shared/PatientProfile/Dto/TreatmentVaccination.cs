using System;
using System.Collections.Generic;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class TreatmentVaccination
    {
        public DateTime Date { get; set; }
        public string Vaccination { get; set; }
        public string DosesAdministered { get; set; }
        public DateTime? DateAdministered { get; set; }
        public DateTime? NextDueDate { get; set; }
        public List<TreatmentVaccinationDetails> Details { get; set; }
    }
}
