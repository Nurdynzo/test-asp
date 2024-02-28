using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Procedures.Dtos;

public class ScheduleProcedureDto
{ 
    public long ProcedureId { get; set; } 
    
    public List<SelectedProcedureDto> Procedures { get; set; }
    
    public bool IsProcedureInSameSession { get; set; }
    
    public long? RoomId { get; set; }
    
    public string Duration { get; set; }
    
    public DateTime? ProposedDate { get; set; }
    
    public string Time { get; set; }
}