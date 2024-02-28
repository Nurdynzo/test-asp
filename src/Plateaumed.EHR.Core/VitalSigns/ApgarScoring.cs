using System.Collections.Generic;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.VitalSigns;

public class ApgarScoring : Entity<long>
{
    public string Name { get; set; }
    public List<ApgarScoringRange> Ranges { get; set; }
}