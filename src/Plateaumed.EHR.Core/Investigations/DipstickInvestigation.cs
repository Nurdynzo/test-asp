using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.Investigations;

public class DipstickInvestigation : Entity<long>
{
    public long? InvestigationId { get; set; }

    [ForeignKey(nameof(InvestigationId))]
    public Investigation Investigation { get; set; }

    public string Parameter { get; set; }

    public List<DipstickRange> Ranges { get; set; }
}