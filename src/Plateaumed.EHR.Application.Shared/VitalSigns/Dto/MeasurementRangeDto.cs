namespace Plateaumed.EHR.VitalSigns.Dto;

public class MeasurementRangeDto
{
    public long Id { get; set; }
    public decimal? Lower { get; set; }
    public decimal? Upper { get; set; }
    public string Unit { get; set; }
    public int? DecimalPlaces { get; set; } 
    public int? MaxLength { get; set; }
}