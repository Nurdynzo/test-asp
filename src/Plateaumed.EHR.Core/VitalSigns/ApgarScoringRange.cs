using Abp.Domain.Entities;

namespace Plateaumed.EHR.VitalSigns;

public class ApgarScoringRange : Entity<long>
{
    public int Score { get; set; }
    public string Result { get; set; }
}