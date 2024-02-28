using System;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.PatientProfile.Dto;
public class CreatePatientTravelHistoryCommand
{
    [Required]
    public int CountryId { get; set; }
    
    //[Required]
    public string City { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int Duration { get; set; }
    
    [Required] 
    public long PatientId { get; set; }

    public UnitOfTime Interval { get; set; }
}