using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto;

public class DipstickRangeDto
{
    public string Unit { get; set; }

    public List<DipstickResultDto> Results { get; set; }
}