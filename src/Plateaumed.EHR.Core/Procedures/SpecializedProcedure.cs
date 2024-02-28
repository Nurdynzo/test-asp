using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Procedures;

public class SpecializedProcedure : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long ProcedureId { get; set; }
    
    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
    
    public long? SnowmedId { get; set; }
    
    public string ProcedureName { get; set; } 
    
    public bool RequireAnaesthetist { get; set; }
    
    public bool IsProcedureInSameSession { get; set; }
    
    public virtual long? AnaesthetistUserId { get; set; }

    [ForeignKey("AnaesthetistUserId")]
    public virtual User AnaesthetistUser { get; set; }
    
    public virtual long? RoomId { get; set; }

    [ForeignKey("RoomId")]
    public Rooms Rooms { get; set; }
    
    public string Duration { get; set; }
    
    public DateTime? ProposedDate { get; set; }
    
    public string Time { get; set; }
}