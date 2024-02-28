using Abp.Domain.Entities;

namespace Plateaumed.EHR.PhysicalExaminations;

public class PatientPhysicalExamSuggestionQualifier : Entity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public long? QualifierId { get; set; }
    public string Name { get; set; }
}