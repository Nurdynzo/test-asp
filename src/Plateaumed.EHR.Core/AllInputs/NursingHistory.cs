using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.AllInputs;

public class NursingHistory : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public string Note { get; set; }
}