using System;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.InputNotes.Dtos;

public class InputNotesSummaryForReturnDto
{
    public long Id { get; set; }
    public string Description { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }
    public ProcedureEntryType? EntryType { get; set; }
    public long? ProcedureId { get; set; }
    
}
