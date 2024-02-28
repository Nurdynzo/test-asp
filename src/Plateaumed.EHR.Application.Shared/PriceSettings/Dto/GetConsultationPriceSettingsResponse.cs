using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class GetConsultationPriceSettingsResponse :EntityDto<long>
    {
        public long FacilityId { get; set; }
        public bool IsFollowUpVisitEnabled { get; set; }
        public int FrequencyOfChargesTimes { get; set; } = 1;
        public int FrequencyOfChargeValue { get; set; } = 1;
        public PriceTimeFrequency UnitOfFrequencyChargeUnit { get; set; }
        public int PercentageOfFrequencyCharges { get; set; }
    }
}
