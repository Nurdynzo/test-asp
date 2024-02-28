using System;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Patients.Dtos;

public record SearchPatientOutput
{
    public string Fullname { get; set; }
    public GenderType GenderType { get; set; }
    public string PatientCode { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public string DateOfBirth { get; set; }
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    public string PictureUrl { get; set; }
    
}
