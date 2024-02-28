using System.Collections.Generic;

namespace Plateaumed.EHR.VitalSigns.Dto;

public class GetAllVitalSignsResponse
{
    public long Id { get; set; }
    public string Sign { get; set; }
    public List<MeasurementSiteDto> Sites { get; set; }
    public List<MeasurementRangeDto> Ranges { get; set; }
    public bool LeftRight { get; set; }
    public int DecimalPlaces { get; set; }
    public bool IsPreset { get; set; }
    public int MaxLength { get; set; }
}