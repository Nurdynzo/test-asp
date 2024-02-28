using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto;

public class GetInvestigationResponse
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string SnomedId { get; set; }

    public string Synonyms { get; set; }

    public string Specimen { get; set; }

    public bool NugentScore { get; set; }

    public List<GetInvestigationResponse> Components { get; set; }

    public List<InvestigationRangeDto> Ranges { get; set; }

    public List<InvestigationSuggestionDto> Suggestions { get; set; }

    public List<InvestigationResultsDto> Results { get; set; }

    public List<DipstickDto> Dipstick { get; set; } = new();
}