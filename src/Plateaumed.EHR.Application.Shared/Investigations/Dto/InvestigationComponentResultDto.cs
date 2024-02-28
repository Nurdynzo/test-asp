namespace Plateaumed.EHR.Investigations.Dto;

public class InvestigationComponentResultDto
{ 
    public string Category { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
    public decimal NumericResult { get; set; }
    public string Reference { get; set; }
    public decimal RangeMin { get; set; }
    public decimal RangeMax { get; set; }
    public string Unit { get; set; }
}