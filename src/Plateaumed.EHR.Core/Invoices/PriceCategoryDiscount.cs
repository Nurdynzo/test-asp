using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.Invoices
{
    public class PriceCategoryDiscount : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long? FacilityId { get; set; }
        public Facility Facility { get; set; }
        public PricingCategory PricingCategory { get; set; }
        public long PricingDiscountSettingId { get; set; }
        public decimal Percentage { get; set; }
        public bool IsActive { get; set; }
    }
}
