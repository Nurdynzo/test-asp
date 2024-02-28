using Abp.Domain.Entities;

namespace Plateaumed.EHR.VitalSigns;

public class MeasurementSite : Entity<long>
{
    public string Site { get; set; }
    public bool Default { get; set; }
}