using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class PriceItemsSearchRequest
    {
        [Required]
        public long FacilityId { get; set; }
        public PricingCategory PricingCategory { get; set; }
        public string SearchTerm { get; set; }
    }
}
