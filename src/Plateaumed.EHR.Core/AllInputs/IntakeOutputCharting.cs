using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.IntakeOutputs;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.AllInputs;

public class IntakeOutputCharting : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }  //
    
    public long PatientId { get; set; }
    
    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public ChartingType Type { get; set; }
    
    public string SuggestedText { get; set; }
    
    public double VolumnInMls { get; set; }
    
    [ForeignKey("CreatorUserId")]
    public virtual User CreatorUser { get; set; }
    
    public long? EncounterId { get; set; }
    
    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }
    
    public long? ProcedureId { get; set; }
    
    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
}