using System;

namespace Plateaumed.EHR.Procedures.Dtos;

public class NoteResponseDto
{
    public long Id { get; set; }
    
    public long ProcedureId { get; set; }
    
    public string Note { get; set; }
    
    public DateTime CreationTime { get; set; }
}