using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Investigations;

public class RadiologyAndPulmonaryInvestigation : Entity<long>
{
    public long InvestigationId { get; set; }

    [ForeignKey(nameof(InvestigationId))]
    public Investigation Investigation { get; set; }

    public string Name { get; set; }

    public string SnomedId { get; set; }

    public string Category { get; set; }
}