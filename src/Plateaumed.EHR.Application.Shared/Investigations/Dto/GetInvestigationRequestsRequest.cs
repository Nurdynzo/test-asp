namespace Plateaumed.EHR.Investigations.Dto;

public class GetInvestigationRequestsRequest
{
    public long PatientId { get; set; }
    public string Type { get; set; }
    public long? ProcedureId { get; set; } = null;

}