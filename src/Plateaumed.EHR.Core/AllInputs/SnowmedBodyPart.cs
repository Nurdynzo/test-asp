using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.AllInputs;

public class SnowmedBodyPart : FullAuditedEntity<long>
{
    public string SnowmedId { get; set; }
    public string Part { get; set; }
    public string SubPart { get; set; }
    public string Synonym { get; set; }
    public string Description { get; set; }
    public string Gender { get; set; }
}