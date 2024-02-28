using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.PatientProfile;

[Table("AllergyReactionExperiencedSuggestions")]
public class AllergyReactionExperiencedSuggestion : FullAuditedEntity<long>
{
    public string ReactionName { get; set; }

    public long? SnomedId { get; set; }
}