using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class UpdateInvestigationPricingRequestCommand
	{
        [Required(ErrorMessage = "Pricing Id is required" )]
        public long InvestigationPricingId { get; set; }

        [Required(ErrorMessage = "Investigation is required")]
        public long InvestigationId { get; set; }

        public MoneyDto Amount { get; set; }

        [Required(ErrorMessage = "Pricing status is required")]
        public bool IsActive { get; set; }

        public string Notes { get; set; }
    }
}

