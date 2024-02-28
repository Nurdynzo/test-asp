using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Procedures;

public class Procedure : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; } 
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public long? SnowmedId { get; set; }
    
    public string SelectedProcedures { get; set; }  
    
    public string Note { get; set; }  
    
    public ProcedureType ProcedureType { get; set; }

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public virtual PatientEncounter Encounter { get; set; }
    
    public long? ParentProcedureId { get; set; } 
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
    
    public ProcedureStatus? ProcedureStatus { get; set; }
}