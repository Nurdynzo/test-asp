using System.Collections.Generic;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.PhysicalExaminations;

public class PatientPhysicalExamSuggestionAnswer : Entity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public string SnowmedId { get; set; }
    public string Description { get; set; }
    public bool IsAbsent { get; set; }
    public List<PatientPhysicalExamSuggestionQualifier> Sites { get; set; }
    public List<PatientPhysicalExamSuggestionQualifier> Planes { get; set; }
    public List<PatientPhysicalExamSuggestionQualifier> Qualifiers { get; set; }
}