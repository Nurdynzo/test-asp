namespace Plateaumed.EHR.Investigations.Dto;

public class LinkInvestigationToDiagnosisRequest
{
    public long InvestigationRequestId { get; set; }
    public long DiagnosisId { get; set; }
}