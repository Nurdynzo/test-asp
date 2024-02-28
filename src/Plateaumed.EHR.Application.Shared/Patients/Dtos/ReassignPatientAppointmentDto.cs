namespace Plateaumed.EHR.Patients.Dtos;

public class ReassignPatientAppointmentDto
{
    public int EncounterId { get; set; }
    public int NewAttendingPhysicianId { get; set; }
}