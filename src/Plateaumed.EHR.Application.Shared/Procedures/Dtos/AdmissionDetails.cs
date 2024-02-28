using System;
namespace Plateaumed.EHR.Procedures.Dtos
{
    public class AdmissionDetails
    { 
        public string Name { get; set; }   
        public DateOnly DateAdmitted { get; set; }
        public int LengthStayed { get; set; }
    }
}