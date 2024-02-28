using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.AllInputs;

public class DischargeMedication : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public long DischargeId { get; set; }
    [ForeignKey("DischargeId")]
    public virtual Discharge Discharge { get; set; }
    public long MedicationId { get; set; }
    [ForeignKey("MedicationId")]
    public virtual Medication Medication { get; set; }
}
