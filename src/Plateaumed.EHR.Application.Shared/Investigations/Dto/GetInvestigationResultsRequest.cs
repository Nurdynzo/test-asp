namespace Plateaumed.EHR.Investigations.Dto;

public class GetInvestigationResultsRequest
{
    public long PatientId { get; set; }
    public string Type { get; set; }
    public string Filter { get; set; }
    
    public long? ProcedureId { get; set; } = null;

}