using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class PriceCategoryDiscountResponse : EntityDto<long>
    {
        public long? FacilityId { get; set; }
        public PricingCategory PricingCategory { get; set; }
        public decimal Percentage { get; set; }
        public bool IsActive { get; set; }
    }
}
