using System;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class TreatmentVaccinationDetails
    {
        public DateTime? NextDueDate { get; set; }
        public int Doses { get; set; }
        public string Brand { get; set; }
        public string BatchNo { get; set; }
        public DateTime? DateAdministered { get; set; }
        public bool HasComplication { get; set; }
        public string Notes { get; set; }
        
    }
}
