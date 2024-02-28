using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Procedures.Dtos;

public class CreateProcedureDto
{ 
    public long? SnowmedId { get; set; }
    
    [Required]
    public long PatientId { get; set; } 
    
    public List<SelectedProcedureDto> SelectedProcedures { get; set; }
    
    public string Note { get; set; } 
    
    [Required]
    public string ProcedureType { get; set; }

    public ProcedureEntryType? ProcedureEntryType { get; set; }
    
    public long EncounterId { get; set; }
    
    public long? ParentProcedureId { get; set; } 
}

public class SelectedProcedureDto
{
    public long? SnowmedId { get; set; }
    
    [Required]
    public string ProcedureName { get; set; } 
}