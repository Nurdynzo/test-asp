using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Invoices;

[Table("PricingDiscountSettings")]
public class PricingDiscountSetting: FullAuditedEntity<long>, IMustHaveTenant
{
    [DefaultValue(0.00)]
    public decimal GlobalDiscount { get; set; }
    public int TenantId { get; set; }
    public long? FacilityId { get; set; }
    public Facility Facility { get; set; }
    public virtual ICollection<PriceCategoryDiscount> PriceCategoryDiscounts { get; set; }
}
