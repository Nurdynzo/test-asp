using System.Collections.Generic;
using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class GetDiscountPriceSettingsResponse : EntityDto<long>
    {
        public decimal GlobalDiscount { get; set; }
        public long? FacilityId { get; set; }
        public IReadOnlyList<PriceCategoryDiscountResponse> PriceCategoryDiscounts { get; set; }
    }
}
