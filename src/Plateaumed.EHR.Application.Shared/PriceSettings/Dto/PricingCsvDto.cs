namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class PricingCsvDto
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ItemId { get; set; }
        public string PricingType { get; set; }
        public string SubCategory { get; set; }
        public string PricingCategory { get; set; }
    }
}