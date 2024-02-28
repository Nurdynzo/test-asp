namespace Plateaumed.EHR.Investigations.Dto;

public class InvestigationSuggestionDto
{
    public string Result { get; set; }
    public string SnomedId { get; set; }
    public string Category { get; set; }
    public bool Normal { get; set; }
}