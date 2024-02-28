using System.Collections.Generic;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.PhysicalExaminations;

public class PatientPhysicalExamSuggestionQuestion : Entity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public string HeaderName { get; set; }
    public List<PatientPhysicalExamSuggestionAnswer> PatientPhysicalExamSuggestionAnswers { get; set; }
}