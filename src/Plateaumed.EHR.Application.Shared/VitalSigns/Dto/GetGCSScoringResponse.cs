using System.Collections.Generic;

namespace Plateaumed.EHR.VitalSigns.Dto;

public class GetGCSScoringResponse
{
    public string Name { get; set; }
    public List<GCSScoringRangeDto> Ranges { get; set; }
}