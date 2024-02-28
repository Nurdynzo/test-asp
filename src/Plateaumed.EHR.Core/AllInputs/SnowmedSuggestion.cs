using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Symptom;

namespace Plateaumed.EHR.AllInputs;

public class SnowmedSuggestion : FullAuditedEntity<long>
{  
    public string SourceSnowmedId { get; set; }
    public string SourceName { get; set; }
    public string Name { get; set; }
    public string SnowmedId { get; set; }
    public AllInputType Type { get; set; } 
}