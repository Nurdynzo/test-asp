using Abp.Domain.Entities;

namespace Plateaumed.EHR.PhysicalExaminations;

public class PatientPhysicalExamTypeNote : Entity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public string Type { get; set; }
    public string Note { get; set; }
}