using System;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class ClinicalInvestigationResponse
    {
        public string TestName { get; set; }
        public string RequestingPhysician { get; set; }
        public string Clinic { get; set; }
        public DateTime DateOfRequest { get; set; }

    }

}
