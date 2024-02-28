using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices;

[Table("ItemPricingCategories")]
public class ItemPricingCategory: FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    [Required]
    [StringLength(maximumLength: InvoiceItemConsts.MaxNameLength, MinimumLength = InvoiceItemConsts.MinNameLength)]
    public string Name { get; set; }

    [DefaultValue(0.00)]
    public decimal DiscountPercentage { get; set; }

    public ICollection<ItemPricing> ItemPricing { get; set; }
    
    public PricingCategory PricingCategory { get; set; }
}