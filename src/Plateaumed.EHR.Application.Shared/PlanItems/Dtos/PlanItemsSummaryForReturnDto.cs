using System;

namespace Plateaumed.EHR.PlanItems.Dtos;

public class PlanItemsSummaryForReturnDto
{
    public long Id { get; set; } 
    public string Description { get; set; }  
    public long? ProcedureId { get; set; }
    public string ProcedureEntryType { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }
    
}
