using System.Collections.Generic;

namespace Plateaumed.EHR.Vaccines.Dto;

public class CreateMultipleVaccinationHistoryDto
{
    public List<CreateVaccinationHistoryDto> VaccinationHistory { get; set; }

    public long EncounterId { get; set; }
}

public class CreateVaccinationHistoryDto
{
    public long PatientId { get; set; } 
    
    public long VaccineId { get; set; } 
    
    public bool HasComplication { get; set; } 
    
    public string LastVaccineDuration { get; set; } 
    
    public string Note { get; set; }

    public long? Id { get; set; }

    public int NumberOfDoses { get; set; }
}