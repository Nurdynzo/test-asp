using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Patients.Dtos;

public class PatientDetailsQueryResponse: EntityDto<long>
{
    public string FullName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string PatientCode { get; set; }
    
    public GenderType Gender { get; set; }

    public MaritalStatus? MaritalStatus { get; set; }

    public string EmailAddress { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Nationality { get; set; }
    
    public int NoOfMaleChildren { get; set; }
    
    public int  NoOfFemaleChildren { get; set; }

    public int  NoOfMaleSiblings { get; set; }

    public int NoOfFemaleSiblings { get; set; }

    public int TotalNoOfSiblings { get; set; }
    
    public int TotalNoOfChildren { get; set; }

    public string PictureUrl { get; set; }
}