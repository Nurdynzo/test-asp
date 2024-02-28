using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class GetPriceListQueryResponse
    {
        public long Id { get; set; }
        public string ItemId { get; set; }
        public string Name { get; set; }
        public PricingCategory PricingCategory { get; set; }
        public MoneyDto Price { get; set; }
        public PricingType PricingType { get; set; }
        public bool IsActive { get; set; }
        public string SubCategory { get; set; }

        public PriceTimeFrequency? Frequency { get; set; }
    }

}
