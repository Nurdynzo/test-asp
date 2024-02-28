using System.Collections.Generic;

namespace Plateaumed.EHR.VitalSigns.Dto;

public class GetApgarScoringResponse
{
    public string Name { get; set; }
    public List<ApgarScoringRangeDto> Ranges { get; set; }
}