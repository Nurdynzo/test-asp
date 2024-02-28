using System;
using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientLandingListOuptDto
    {
        public long Id { get; set; }

        public long EncounterId { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PatientCode { get; set; }

        public long PatientId { get; set; }

        public string Clinic { get; set; }

        public GenderType Gender { get; set; }

        public AppointmentStatusType Status { get; set; }

        public AppointmentType AppointmentType { get; set; }

        public DateTime StartTime { get; set; }

        public string AttendingPhysicianStaffCode { get; set; }

        public string AttendingPhysician { get; set; }

        public List<PatientVitalsDto> PatientVitals { get; set; }

        public List<string> Diagnosis { get; set; }

        public string PaymentStatus { get; set; }

        public string PictureUrl { get; set; }       
    }
}