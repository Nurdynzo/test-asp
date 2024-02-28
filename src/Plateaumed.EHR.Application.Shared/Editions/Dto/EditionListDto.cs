using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Editions.Dto
{
    public class EditionListDto : EntityDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; } 

        public decimal? MonthlyPrice { get; set; }

        public decimal? AnnualPrice { get; set; }

        public decimal? Discount { get; set; }

        public int? WaitingDayAfterExpire { get; set; }

        public int? TrialDayCount { get; set; }

        public string ExpiringEditionDisplayName { get; set; }
        
        public int? TenantId { get; set; }
        
        public string TenantName { get; set; } 
        
        public int? CountryId { get; set; }
        
        public string CountryName { get; set; } 
    }
}