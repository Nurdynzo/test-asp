namespace Plateaumed.EHR.VitalSigns.Dto;

public class GetSimpleVitalSignsResponse
{
    public long Id { get; set; }
    
    public string Sign { get; set; } 
    
    public bool LeftRight { get; set; }
    
    public int DecimalPlaces { get; set; }
    
    public bool IsPreset { get; set; }
    
    public int MaxLength { get; set; }
}