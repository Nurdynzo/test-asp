using System;
using System.Collections.Generic;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.Vaccines.Dto;


public class CreateMultipleVaccinationDto
{
    public List<CreateVaccinationDto> Vaccinations { get; set; }

    public long EncounterId { get; set; }

    public long? ProcedureId { get; set; } = null;
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
}

public class CreateVaccinationDto
{
    public DateTime? DueDate { get; set; }
    public long PatientId { get; set; }
    
    public long VaccineId { get; set; } 
    
    public long VaccineScheduleId { get; set; } 
    
    public bool IsAdministered { get; set; } 
    
    public DateTime? DateAdministered { get; set; }
    
    public bool HasComplication { get; set; } 
    
    public string VaccineBrand { get; set; }
    
    public string VaccineBatchNo { get; set; }
    
    public string Note { get; set; }
}