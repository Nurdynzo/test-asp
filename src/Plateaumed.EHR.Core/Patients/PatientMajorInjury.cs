using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Patients;

public class PatientMajorInjury : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public string Diagnosis { get; set; }

    public int  PeriodOfInjury { get; set; }

    public UnitOfTime PeriodOfInjuryUnit { get; set; }

    public bool IsOngoing { get; set; }

    public bool IsComplicationPresent { get; set; }
    
    
    public string Notes { get; set; }
}