using System;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class GetInvestigationPricingResponseDto
	{
        public long Id { get; set; }

        public long InvestigationId { get; set; }

        public string NameOfInvestigation { get; set; }

        public string TypeOfInvestigation { get; set; }

        public MoneyDto Amount { get; set; }
        
        public bool IsActive { get; set; }

        public string Notes { get; set; }
        
        public DateTime DateCreated { get; set; }
    }
}

