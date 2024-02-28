using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Feeding;

namespace Plateaumed.EHR.AllInputs;

[Table("FeedingSuggestions")]
[Audited]
public class FeedingSuggestions : FullAuditedEntity<long>
{
    public string SnowmedId { get; set; }
    public FeedingType Type { get; set; }
    public string Name { get; set; }
}