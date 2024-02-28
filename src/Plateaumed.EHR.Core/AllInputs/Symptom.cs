using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Symptom;

namespace Plateaumed.EHR.AllInputs;

[Table("Symptoms")]
[Audited]
public class Symptom : FullAuditedEntity<long>
{
    public SymptomEntryType SymptomEntryType { get; set; }
    public int? TenantId { get; set; }
    
    public long? Stamp { get; set; }
   
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public string SymptomSnowmedId { get; set; }
    
    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }
    public string OtherNote { get; set; } 
    public string JsonData { get; set; } 

    [ForeignKey("CreatorUserId")]
    public virtual User CreatorUser { get; set; } 

    [ForeignKey("LastModifierUserId")]
    public virtual User LastModifierUser { get; set; } 
    
    [ForeignKey("DeleterUserId")]
    public virtual User DeleterUser { get; set; }  

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public virtual PatientEncounter Encounter { get; set; }
}