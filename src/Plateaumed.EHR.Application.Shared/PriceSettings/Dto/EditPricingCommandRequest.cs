using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class EditPricingCommandRequest : EntityDto<long>
    {
        public string Name { get; set; }
        public MoneyDto Amount { get; set; }
        public string ItemId { get; set; }
        public PricingType PricingType { get; set; }
        public string SubCategory { get; set; }
        public PricingCategory PricingCategory { get; set; }
        public PriceTimeFrequency? Frequency { get; set; }
    }
}
