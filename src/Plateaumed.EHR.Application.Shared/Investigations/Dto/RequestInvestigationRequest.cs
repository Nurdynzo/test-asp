namespace Plateaumed.EHR.Investigations.Dto;

public class RequestInvestigationRequest
{
    public long PatientId { get; set; }

    public long InvestigationId { get; set; }

    public bool Urgent { get; set; }

    public bool WithContrast { get; set; }

    public string Notes { get; set; }

    public long? EncounterId { get; set; }
    
    public long? ProcedureId { get; set; } = null;

}