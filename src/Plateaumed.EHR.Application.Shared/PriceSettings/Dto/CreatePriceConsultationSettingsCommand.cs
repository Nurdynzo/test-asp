using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class CreatePriceConsultationSettingsCommand: EntityDto<long>
    {
        [Required]
        public long FacilityId { get; set; }
        public bool IsFollowUpVisitEnabled { get; set; }
        public int FrequencyOfChargesTimes { get; set; }
        public int FrequencyOfChargeValue { get; set; }
        public PriceTimeFrequency UnitOfFrequencyChargeUnit { get; set; }
        public int PercentageOfFrequencyCharges { get; set; }
    }

}
