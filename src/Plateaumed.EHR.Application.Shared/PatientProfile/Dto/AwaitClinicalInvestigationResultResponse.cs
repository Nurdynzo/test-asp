using System;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class AwaitClinicalInvestigationResultResponse
    {
        public string Name  { get; set; }
        public string  Physician { get; set; }
        public string  Clinic { get; set; }
        public DateTime Date { get; set; }
    }
}