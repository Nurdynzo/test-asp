using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Editions.Dto
{
    public class EditionCreateDto
    {
        public int? Id { get; set; }

        [Required]
        public string DisplayName { get; set; }    

        public decimal? MonthlyPrice { get; set; }

        public decimal? AnnualPrice { get; set; }

        public decimal? Discount { get; set; }

        public int? TrialDayCount { get; set; }

        public int? WaitingDayAfterExpire { get; set; }

        public int? ExpiringEditionId { get; set; }
        
        public int? TenantId { get; set; }
        
        public int? CountryId { get; set; }
    }
}