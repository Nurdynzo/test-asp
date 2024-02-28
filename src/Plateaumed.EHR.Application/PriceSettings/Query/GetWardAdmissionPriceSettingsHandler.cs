using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Query
{
    public class GetWardAdmissionPriceSettingsHandler : IGetWardAdmissionPriceSettingsHandler
    {
        private readonly IRepository<PriceWardAdmissionSetting, long> _priceWardAdmissionSettingRepository;
        public GetWardAdmissionPriceSettingsHandler(IRepository<PriceWardAdmissionSetting, long> priceWardAdmissionSettingRepository)
        {
            _priceWardAdmissionSettingRepository = priceWardAdmissionSettingRepository;
        }
        public async Task<GetWardAdmissionPriceSettingsResponse> Handle(long facilityId)
        {
            var query = await (from c in _priceWardAdmissionSettingRepository.GetAll()
                         where c.FacilityId == facilityId
                         select new GetWardAdmissionPriceSettingsResponse
                         {
                             Id = c.Id,
                             FacilityId = c.FacilityId,
                             DefaultInitialPeriodValue = c.DefaultInitialPeriodValue,
                             DefaultInitialPeriodUnit = c.DefaultInitialPeriodUnit,
                             DefaultContinuePeriodUnit = c.DefaultContinuePeriodUnit,
                             DefaultContinuePeriodValue = c.DefaultContinuePeriodValue,
                             AdmissionChargeTime = c.AdmissionChargeTime,
                             FollowUpVisitPercentage = c.FollowUpVisitPercentage

                         }).FirstOrDefaultAsync().ConfigureAwait(false);

            return query ?? new GetWardAdmissionPriceSettingsResponse()
            {
                FacilityId = facilityId,
                DefaultInitialPeriodValue = 0,
                DefaultInitialPeriodUnit = PriceTimeFrequency.Day,
                DefaultContinuePeriodUnit = PriceTimeFrequency.Day,
                DefaultContinuePeriodValue = 0,
                AdmissionChargeTime = new TimeOnly(12, 0, 0),
                FollowUpVisitPercentage = 0
            };
        }
    }
}
