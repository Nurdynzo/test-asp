using System;

namespace Plateaumed.EHR.Procedures.Dtos;

public class NoteTemplateResponseDto
{
    public long Id { get; set; }
    
    public NoteType NoteType { get; set; }
    
    public string NoteTypeName { get; set; }
    
    public string NoteTitle { get; set; }
    
    public string Note { get; set; }
    
    public DateTime CreationTime { get; set; }
}