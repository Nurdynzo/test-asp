namespace Plateaumed.EHR.PatientAppointments.Dtos;

public record AttendingClinicDto
{
    public long Id { get; set; }
    public string DisplayName { get; set; }
}