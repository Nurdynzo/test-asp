namespace Plateaumed.EHR.PlanItems.Dtos;

public class GetPlanItemsDto
{
    public int PatientId { get; set; }
    public long? ProcedureId { get; set; }
}