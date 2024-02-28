using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.Investigations;

public class InvestigationSuggestion : Entity<long>
{
    public long? InvestigationId { get; set; }

    [ForeignKey(nameof(InvestigationId))]
    public Investigation Investigation { get; set; }

    public string Category { get; set; }

    public string Result { get; set; }

    public string SnomedId { get; set; }

    public bool Normal { get; set; }
}