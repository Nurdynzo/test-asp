namespace Plateaumed.EHR.Snowstorm.Dtos;

public class SnowstormRequestDto
{
    public string Term { get; set; }
    public string SemanticTag { get; set; }
    public string SemanticTag2 { get; set; } = default;
}