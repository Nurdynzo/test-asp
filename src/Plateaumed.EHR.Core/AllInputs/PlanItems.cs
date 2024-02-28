using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using Abp.Domain.Entities;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.AllInputs;

[Table("PlanItems")]
[Audited]
public class PlanItems : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long? Stamp { get; set; }
      
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public List<string> PlanItemsSnowmedIds { get; set; }
    
    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }
    
    public long? ProcedureId { get; set; }

    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public virtual PatientEncounter Encounter { get; set; }
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
}