using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.PhysicalExaminations;

public class PatientPhysicalExamination : FullAuditedEntity<long>, IMustHaveTenant
{
    public PhysicalExaminationEntryType PhysicalExaminationEntryType { get; set; }
    
    public int TenantId { get; set; }
    
    public long PhysicalExaminationTypeId { get; set; }
    
    [ForeignKey("PhysicalExaminationTypeId")]
    public virtual PhysicalExaminationType PhysicalExaminationType { get; set; }
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public string OtherNote { get; set; } 
    
    public List<PatientPhysicalExamTypeNote> TypeNotes { get; set; }
    
    public List<PatientPhysicalExamSuggestionQuestion> Suggestions { get; set; }
    
    [ForeignKey("CreatorUserId")]
    public virtual User CreatorUser { get; set; } 

    [ForeignKey("LastModifierUserId")]
    public virtual User LastModifierUser { get; set; } 
    
    [ForeignKey("DeleterUserId")]
    public virtual User DeleterUser { get; set; }

    public long EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }
    
    public long? ProcedureId { get; set; }
    
    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
    
    
}