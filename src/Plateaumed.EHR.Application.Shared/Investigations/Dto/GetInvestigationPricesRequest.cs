using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class GetInvestigationPricesRequest
	{
        public List<long> InvestigationIds { get; set; }
    }
}