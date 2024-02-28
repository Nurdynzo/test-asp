using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto;

public class DipstickDto
{
    public string Parameter { get; set; }

    public List<DipstickRangeDto> Ranges { get; set; }
}