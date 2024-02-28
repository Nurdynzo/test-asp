using System;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.PatientAppointments.Dtos;

public record AppointmentPatientDto
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string PatientCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public GenderType GenderType { get; set; }

    public string PictureUrl { get; set; }

    public bool IsNewToHospital { get; set; }
}