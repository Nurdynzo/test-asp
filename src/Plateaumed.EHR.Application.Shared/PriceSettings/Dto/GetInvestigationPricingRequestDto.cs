using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class GetInvestigationPricingRequestDto : PagedResultRequestDto
    {
        public string TestName { get; set; }
        public string InvestigationType { get; set; }
        public string SortBy { get; set; }
        public long InvestigationPricingId { get; set; }
    }
}

