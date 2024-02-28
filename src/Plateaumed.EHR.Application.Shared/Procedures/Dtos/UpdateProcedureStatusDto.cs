namespace Plateaumed.EHR.Procedures.Dtos;

public class UpdateProcedureStatusDto
{
    public long ProcedureId { get; set; }
    
    public ProcedureStatus ProcedureStatus { get; set; }
}