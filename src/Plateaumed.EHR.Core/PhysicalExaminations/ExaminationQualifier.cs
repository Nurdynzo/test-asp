using Abp.Domain.Entities;

namespace Plateaumed.EHR.PhysicalExaminations;

public class ExaminationQualifier : Entity<long>
{
    public string SubQualifier { get; set; }
    public string SubDivision { get; set; }
    public string Qualifier { get; set; }
    public string SnomedId { get; set; }
}