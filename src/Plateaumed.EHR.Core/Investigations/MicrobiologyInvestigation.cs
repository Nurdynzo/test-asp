using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.Investigations;

public class MicrobiologyInvestigation : Entity<long>
{
    public long? InvestigationId { get; set; }

    [ForeignKey(nameof(InvestigationId))]
    public Investigation Investigation { get; set; }

    public bool MethyleneBlueStain { get; set; } = new();

    public bool AntibioticSensitivityTest { get; set; }
    
    public bool NugentScore { get; set; }

    public bool Culture { get; set; }

    public bool GramStain { get; set; }

    public bool Microscopy { get; set; }

    public bool CommonResults { get; set; }
}