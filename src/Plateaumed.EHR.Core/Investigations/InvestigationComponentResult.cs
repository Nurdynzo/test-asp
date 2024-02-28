using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Investigations;

public class InvestigationComponentResult : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long InvestigationResultId { get; set; }

    [ForeignKey(nameof(InvestigationResultId))]
    public InvestigationResult InvestigationResult { get; set; }

    public string Category { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
    public decimal NumericResult { get; set; }
    public string Reference { get; set; }
    public decimal RangeMin { get; set; }
    public decimal RangeMax { get; set; }
    public string Unit { get; set; }
}