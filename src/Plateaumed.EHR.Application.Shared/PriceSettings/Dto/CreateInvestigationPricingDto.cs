using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class CreateInvestigationPricingDto
	{
        [Required(ErrorMessage = "Investigation is required")]
        public long InvestigationId { get; set; }
        
        public MoneyDto Amount { get; set; }

        [Required(ErrorMessage = "Pricing status is required")]
        public bool IsActive { get; set; }

        public string Notes { get; set; }
       
    }
}

