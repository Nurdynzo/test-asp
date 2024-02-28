using System.Collections.Generic;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class GetInvestigationPricessResponse
	{
        public Dictionary<long, MoneyDto> InvestigationsAndPrices { get; set; }
    }
}