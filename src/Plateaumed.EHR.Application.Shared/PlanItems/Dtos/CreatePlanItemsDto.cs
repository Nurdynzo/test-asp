using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.PlanItems.Dtos;

public class CreatePlanItemsDto
{
    public long PatientId { get; set; }
    public long? ProcedureId { get; set; }
    public long? Stamp { get; set; }
    public List<string> PlanItemsSnowmedIds { get; set; }
    public long EncounterId { get; set; }

    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
}
