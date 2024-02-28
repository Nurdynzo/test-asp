using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Query
{
    public class GetPriceMealSettingsHandler : IGetPriceMealSettingsHandler
    {
        private readonly IRepository<PriceMealSetting, long> _priceMealSettingRepository;
        public GetPriceMealSettingsHandler(IRepository<PriceMealSetting, long> priceMealSettingRepository)
        {
            _priceMealSettingRepository = priceMealSettingRepository;
        }
        public async Task<GetPriceMealSettingsResponse> Handle(long facilityId)
        {
            var query = await (from c in _priceMealSettingRepository.GetAll().Include(x => x.MealPlanDefinitions)
                               where c.FacilityId == facilityId
                               select new GetPriceMealSettingsResponse
                               {
                                   Id = c.Id,
                                   FacilityId = c.FacilityId,
                                   DefaultInitialPeriodValue = c.DefaultInitialPeriodValue,
                                   DefaultInitialPeriodUnit = c.DefaultInitialPeriodUnit,
                                   DefaultContinuePeriodUnit = c.DefaultContinuePeriodUnit,
                                   DefaultContinuePeriodValue = c.DefaultContinuePeriodValue,
                                   IsMealPlanEnabled = c.IsMealPlanEnabled,
                                   MealPlanDefinitions = c.MealPlanDefinitions.Select(x => new PriceMealPlanDefinitionResponse
                                   {
                                       PlanType = x.PlanType,
                                       Description = x.Description,
                                       PriceMealSettingId = x.PriceMealSettingId,
                                       FacilityId = x.FacilityId,
                                       Id = x.Id,
                                       IsActive = x.IsActive,
                                       IsDefault = x.IsDefault

                                   }).ToList()

                               }).FirstOrDefaultAsync().ConfigureAwait(false);
            return query ?? new GetPriceMealSettingsResponse
            {
                FacilityId = facilityId,
                DefaultInitialPeriodValue = 0,
                DefaultInitialPeriodUnit = PriceTimeFrequency.Day,
                DefaultContinuePeriodUnit = PriceTimeFrequency.Day,
                DefaultContinuePeriodValue = 0,
                IsMealPlanEnabled = false,
                MealPlanDefinitions = new List<PriceMealPlanDefinitionResponse>()
            };
        }
    }
}
