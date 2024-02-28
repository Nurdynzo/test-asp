using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.VitalSigns;

public class PatientVital : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public long VitalSignId { get; set; }
    
    [ForeignKey("VitalSignId")]
    public virtual VitalSign VitalSign { get; set; }
    
    public long? MeasurementSiteId { get; set; }
    
    [ForeignKey("MeasurementSiteId")]
    public virtual MeasurementSite MeasurementSite { get; set; }
    
    public long? MeasurementRangeId { get; set; }
    
    [ForeignKey("MeasurementRangeId")]
    public virtual MeasurementRange MeasurementRange { get; set; }
    
    public PatientVitalPosition? VitalPosition { get; set; } 
    
    public double VitalReading { get; set; }
    
    public long? ProcedureId { get; set; }
    
    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
    
    [ForeignKey("CreatorUserId")]
    public virtual User CreatorUser { get; set; }
    
    [ForeignKey("LastModifierUserId")]
    public virtual User LastModifierUser { get; set; }
    
    [ForeignKey("DeleterUserId")]
    public virtual User DeleterUser { get; set; }
    
    public PatientVitalType PatientVitalType { get; set; }

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public virtual PatientEncounter Encounter { get; set; }
}