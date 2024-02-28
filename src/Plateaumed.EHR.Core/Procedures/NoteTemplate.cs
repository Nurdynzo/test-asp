using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Procedures;

public class NoteTemplate : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public NoteType NoteType { get; set; }
    
    public string NoteTitle { get; set; }
    
    public string Note { get; set; }
}