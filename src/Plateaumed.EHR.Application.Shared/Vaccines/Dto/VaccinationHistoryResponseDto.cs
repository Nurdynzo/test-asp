using System;

namespace Plateaumed.EHR.Vaccines.Dto;

public class VaccinationHistoryResponseDto
{ 
    public long Id { get; set; }
    public long PatientId { get; set; } 
    
    public long VaccineId { get; set; } 
    public GetVaccineResponse Vaccine { get; set; }
    public int NumberOfDoses { get; set; }
    public bool HasComplication { get; set; } 
    
    public string LastVaccineDuration { get; set; } 
    
    public string Note { get; set; } 
    
    DateTime CreationTime { get; set; }
}