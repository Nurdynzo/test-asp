using System;

namespace Plateaumed.EHR.Patients.Dtos;

public class PatientTravelHistoryResponse
{
    public string Country { get; set; }
    public int CountryId { get; set; }
    public string City { get; set; }

    public string Duration { get; set; }

    public DateTime Date { get; set; }

    public string CreatedBy { get; set; }
    
    public DateTime DateCreated { get; set; }

    public long Id { get; set; }

}