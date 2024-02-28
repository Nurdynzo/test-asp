using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Procedures.Dtos;

public class PatientProcedureResponseDto
{ 
    public long Id { get; set; }   
    
    public long? SnowmedId { get; set; } 
    
    public long PatientId { get; set; } 
    
    public List<SelectedProcedureDto> SelectedProcedures { get; set; }
    
    public string Note { get; set; }  
    
    public string ProcedureType { get; set; }
    
    public List<SpecializedProcedureResponseDto> SpecializedProcedures { get; set; }
    
    public List<ScheduledProcedureResponseDto> ScheduledProcedures { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public string ProcedureEntryType { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public string DeletedUser { get; set; }
    
    public ProcedureStatus? ProcedureStatus { get; set; }
}
