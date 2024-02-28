using System.Collections.Generic;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.VitalSigns
{
    public class VitalSign : Entity<long>
    {
        public string Sign { get; set; }
        public List<MeasurementSite> Sites { get; set; }
        public List<MeasurementRange> Ranges { get; set; }
        public bool LeftRight { get; set; }
        public int DecimalPlaces { get; set; }
        public bool IsPreset { get; set; }
        public int MaxLength { get; set; }
    }
}
