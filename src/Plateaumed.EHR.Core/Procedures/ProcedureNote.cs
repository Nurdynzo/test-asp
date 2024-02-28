using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Procedures;

public class ProcedureNote : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; } 
    
    public long ProcedureId { get; set; }

    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
    
    public string Note { get; set; }

    public NoteType NoteType { get; set; } = NoteType.ProcedureNote;
}
