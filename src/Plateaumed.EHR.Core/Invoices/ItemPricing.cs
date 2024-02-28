using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Invoices;

[Table("ItemPricing")]
public class ItemPricing : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    [StringLength(maximumLength: InvoiceItemConsts.MaxNameLength, MinimumLength = InvoiceItemConsts.MinNameLength)]
    public string Name { get; set; }

    [DefaultValue(0.00)]
    [Precision(18, 2)]
    public Money Amount { get; set; }

    public virtual long? ItemPricingCategoryId { get; set; }

    [ForeignKey("ItemPricingCategoryId")]
    public virtual ItemPricingCategory ItemPricingCategory { get; set; }

    public long? FacilityId { get; set; }

    [ForeignKey("FacilityId")]
    public Facility Facility { get; set; }

    public bool IsActive { get; set; }

    [StringLength(GeneralStringLengthConstant.MaxStringLength)]
    public string ItemId { get; set; }

    public PricingType PricingType { get; set; }

    [StringLength(GeneralStringLengthConstant.MaxStringLength)]
    public string SubCategory { get; set; }
    
    public PricingCategory PricingCategory { get; set; }

    public PriceTimeFrequency? Frequency { get; set; }
    
}
