using Abp.Domain.Entities;

namespace Plateaumed.EHR.VitalSigns;

public class MeasurementRange : Entity<long>
{
    public decimal? Lower { get; set; }
    public decimal? Upper { get; set; }
    public string Unit { get; set; }
    public int? DecimalPlaces { get; set; } 
    public int? MaxLength { get; set; }
}