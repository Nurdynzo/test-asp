using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Staff;

public class JobServiceCentre : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long JobId { get; set; }

    [ForeignKey("JobId")]
    public Job Job { get; set; }

    public ServiceCentreType ServiceCentre { get; set; }
}