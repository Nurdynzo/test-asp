using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Dto;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PriceSettings.Abstraction;
namespace Plateaumed.EHR.PriceSettings.Query
{
    public class GetConsultationPriceSettingsHandler : IGetConsultationPriceSettingsHandler
    {
        private readonly IRepository<PriceConsultationSetting,long> _priceConsultationSettingRepository;
        public GetConsultationPriceSettingsHandler(IRepository<PriceConsultationSetting, long> priceConsultationSettingRepository)
        {
            _priceConsultationSettingRepository = priceConsultationSettingRepository;
        }
        public async Task<GetConsultationPriceSettingsResponse> Handle(long facilityId)
        {
            var query = await (from c in _priceConsultationSettingRepository.GetAll()
                         where c.FacilityId == facilityId
                         select new GetConsultationPriceSettingsResponse
                         {
                             Id = c.Id,
                             FacilityId = c.FacilityId,
                             IsFollowUpVisitEnabled = c.IsFollowUpVisitEnabled,
                             FrequencyOfChargesTimes = c.FrequencyOfChargesTimes ==0? 1:c.FrequencyOfChargesTimes,
                             FrequencyOfChargeValue = c.FrequencyOfChargeValue == 0 ? 1 : c.FrequencyOfChargeValue,
                             UnitOfFrequencyChargeUnit = c.UnitOfFrequencyChargeUnit,
                             PercentageOfFrequencyCharges = c.PercentageOfFrequencyCharges
                         }).FirstOrDefaultAsync().ConfigureAwait(false);
            return query ?? new GetConsultationPriceSettingsResponse()
            {
                FacilityId = facilityId,
                IsFollowUpVisitEnabled = false,
                FrequencyOfChargesTimes = 0,
                FrequencyOfChargeValue = 0,
                UnitOfFrequencyChargeUnit = PriceTimeFrequency.Day,
                PercentageOfFrequencyCharges = 0
            };
        }
    }
}
