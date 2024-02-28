using System;

namespace Plateaumed.EHR.Procedures.Dtos;

public class SpecializedProcedureResponseDto
{
    public long Id { get; set; }
    public long ProcedureId { get; set; }
    public long? SnowmedId { get; set; }
    public string ProcedureName { get; set; }
    public bool RequireAnaesthetist { get; set; }
    public bool IsProcedureInSameSession { get; set; }
    public long? AnaesthetistUserId { get; set; }
    public virtual long? RoomId { get; set; }
    public virtual string RoomName { get; set; }
    public string Duration { get; set; }
    public DateTime? ProposedDate { get; set; }
    public string Time { get; set; }
    public DateTime CreationTime { get; set; }
    public bool IsDeleted { get; set; }
}
