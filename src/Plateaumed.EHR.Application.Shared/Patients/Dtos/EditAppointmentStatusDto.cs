namespace Plateaumed.EHR.Patients.Dtos
{
    public class EditAppointmentStatusDto
    {
        public long Id { get; set; }
        public AppointmentStatusType Status { get; set; }
    }
}