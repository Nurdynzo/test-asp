using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class GetPriceListQueryRequest : PagedResultRequestDto
    {
        [Required]
        public long FacilityId { get; set; }
        public string SearchText { get; set; }
        public bool? IsActive { get; set; }

        public PricingCategory? PricingCategory { get; set; }

        public PricingType? PricingType { get; set; }
    }
}
