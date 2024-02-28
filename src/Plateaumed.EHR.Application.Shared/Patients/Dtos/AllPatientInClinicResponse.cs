using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.Patients.Dtos
{
    public class AllPatientInClinicResponse
    {
        public string PictureUrl { get; set; }
        public long PatientId { get; set; }
        public string FullName { get; set; }
        public AppointmentStatusType AppointmentStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string AssignedDoctor { get; set; }
        public string Clinic { get; set; }
        public long AppointmentId { get; set; }
        public long EncounterId { get; set; }
    }

}
