using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using Abp.Domain.Entities;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.AllInputs;

[Table("WoundDressing")]
[Audited]
public class WoundDressing : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long? Stamp { get; set; }
  
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public List<string> WoundDressingSnowmedIds { get; set; }
    
    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public virtual PatientEncounter Encounter { get; set; }
}