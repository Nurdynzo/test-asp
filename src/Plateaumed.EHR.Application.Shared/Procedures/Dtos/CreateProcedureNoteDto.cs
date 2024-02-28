namespace Plateaumed.EHR.Procedures.Dtos;

public class CreateProcedureNoteDto
{ 
    public long? TemplateId { get; set; }
    
    public long ProcedureId { get; set; }
    
    public string Note { get; set; }

    public NoteType NoteType { get; set; } = NoteType.ProcedureNote;
}
