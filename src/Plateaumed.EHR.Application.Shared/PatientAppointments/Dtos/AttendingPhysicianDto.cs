using System.Collections.Generic;
namespace Plateaumed.EHR.PatientAppointments.Dtos;

public record AttendingPhysicianDto
{
    public long Id { get; set; }
    public string StaffCode { get; set; }
    public string FullName { get; set; }
    public List<string> Roles { get; set; }
}
