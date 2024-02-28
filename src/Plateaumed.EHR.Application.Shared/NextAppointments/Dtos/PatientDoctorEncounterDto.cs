using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.NextAppointments.Dtos
{
    public class PatientDoctorEncounterDto
    {
        public long Id { get; set; }
        [Required]
        public int TenantId { get; set; }
        [Required]
        public long PatientId { get; set; }
        [Required]
        public long StaffId { get; set; }
        [Required]
        public long? AppointmentId { get; set; }
        [Required]
        public DateTime? TimeIn { get; set; }
        [Required]
        public DateTime? TimeOut { get; set; }
        [Required]
        public EncounterStatusType? Status { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? AdmissionId { get; set; }
        public long? FacilityId { get; set; }
        public ServiceCentreType ServiceCentre { get; set; }
        public long? UnitId { get; set; }
    }
}
