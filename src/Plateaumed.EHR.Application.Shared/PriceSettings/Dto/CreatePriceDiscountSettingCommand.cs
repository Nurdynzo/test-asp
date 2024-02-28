using System.Collections.Generic;
using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class CreatePriceDiscountSettingCommand : EntityDto<long>
    {
        public decimal GlobalDiscount { get; set; }
        public long FacilityId { get; set; }
        public virtual List<PriceCategoryDiscountCommand> PriceCategoryDiscounts { get; set; }
    }
}
