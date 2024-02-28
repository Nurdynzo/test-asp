using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.Vaccines;

public class Vaccination : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public long VaccineId { get; set; } 
    
    [ForeignKey("VaccineId")]
    public virtual Vaccine Vaccine { get; set; }
    
    public long VaccineScheduleId { get; set; } 
    
    [ForeignKey("VaccineScheduleId")]
    public virtual VaccineSchedule VaccineSchedule { get; set; }
    
    public bool IsAdministered { get; set; } 
    
    public DateTime? DueDate { get; set; }
    
    public DateTime? DateAdministered { get; set; }
    
    public bool HasComplication { get; set; } 
    
    public string VaccineBrand { get; set; }
    
    public string VaccineBatchNo { get; set; }
    
    public string Note { get; set; } 

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }
    
    public long? ProcedureId { get; set; }
    
    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
}