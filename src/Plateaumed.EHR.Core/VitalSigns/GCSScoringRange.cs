using Abp.Domain.Entities;

namespace Plateaumed.EHR.VitalSigns;

public class GCSScoringRange : Entity<long>
{
    public int Score { get; set; }
    public int AgeMin { get; set; }
    public int? AgeMax { get; set; }
    public string Response { get; set; }
}