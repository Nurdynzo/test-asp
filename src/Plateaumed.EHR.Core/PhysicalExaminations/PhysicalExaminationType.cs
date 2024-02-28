using Abp.Domain.Entities;

namespace Plateaumed.EHR.PhysicalExaminations;

public class PhysicalExaminationType : Entity<long>
{
    public string Name { get; set; }
    public string Type { get; set; }
}