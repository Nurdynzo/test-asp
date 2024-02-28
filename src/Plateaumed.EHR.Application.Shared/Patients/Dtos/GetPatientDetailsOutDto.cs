using Plateaumed.EHR.Authorization.Users;
using System;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientDetailsOutDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public string PatientCode { get; set; }
        public int LengthOfStayDays { get; set; }
        public BloodGroup? BloodGroup { get; set; }
        public BloodGenotype? BloodGenotype { get; set; }
        public DateTime? LastSeenByDoctor { get; set; }
        public string LastSeenByDoctorName { get; set; } 
        public DateTime? LastSeenByNurse { get; set; }
        public string LastSeenByNurseName { get; set; } 
        public DateTime? DateAdmitted { get; set; }
        public string PictureUrl { get; set; }
        public EncounterStatusType? EncounterStatus { get; set; }
        public int? DaysPostOp { get; set; }
    }
}
