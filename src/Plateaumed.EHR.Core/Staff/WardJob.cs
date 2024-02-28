using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Staff;

public class WardJob : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long WardId { get; set; }

    [ForeignKey("WardId")]
    public Ward Ward { get; set; }

    public long JobId { get; set; }

    [ForeignKey("JobId")]
    public Job Job { get; set; }
}