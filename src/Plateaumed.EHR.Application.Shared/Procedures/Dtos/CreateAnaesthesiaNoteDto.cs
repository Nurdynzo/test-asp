namespace Plateaumed.EHR.Procedures.Dtos;

public class CreateAnaesthesiaNoteDto
{
    public long? TemplateId { get; set; }
    
    public long ProcedureId { get; set; }
    
    public string Note { get; set; }
}