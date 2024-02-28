using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.Investigations;

public class InvestigationBinaryResult : Entity<long>
{
    public long InvestigationId { get; set; }

    [ForeignKey(nameof(InvestigationId))]
    public Investigation Investigation { get; set; }

    public string Result { get; set; }

    public bool Normal { get; set; }
}
