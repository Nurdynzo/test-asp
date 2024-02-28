using System.Collections.Generic;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.VitalSigns
{
    public class GCSScoring : Entity<long>
    {
        public string Name { get; set; }
        public List<GCSScoringRange> Ranges { get; set; }
    }
}
