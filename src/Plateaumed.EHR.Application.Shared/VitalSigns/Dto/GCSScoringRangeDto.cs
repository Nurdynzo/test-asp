namespace Plateaumed.EHR.VitalSigns.Dto;

public class GCSScoringRangeDto
{
    public int Score { get; set; }
    public int AgeMin { get; set; }
    public int? AgeMax { get; set; }
    public string Response { get; set; }
}