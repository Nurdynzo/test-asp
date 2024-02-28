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
    public class GetDiscountPriceSettingsHandler : IGetDiscountPriceSettingsHandler
    {
        private readonly IRepository<PricingDiscountSetting, long> _priceDiscountSettingRepository;
        public GetDiscountPriceSettingsHandler(IRepository<PricingDiscountSetting, long> priceDiscountSettingRepository)
        {
            _priceDiscountSettingRepository = priceDiscountSettingRepository;
        }
        public async Task<GetDiscountPriceSettingsResponse> Handle(long facilityId)
        {
            var query = await (from c in _priceDiscountSettingRepository.GetAll().Include(x => x.PriceCategoryDiscounts)
                               where c.FacilityId == facilityId
                               select new GetDiscountPriceSettingsResponse
                               {
                                   Id = c.Id,
                                   GlobalDiscount = c.GlobalDiscount,
                                   FacilityId = c.FacilityId,
                                   PriceCategoryDiscounts = c.PriceCategoryDiscounts.Select(x => new PriceCategoryDiscountResponse
                                   {
                                       FacilityId = x.FacilityId,
                                       PricingCategory = x.PricingCategory,
                                       Percentage = x.Percentage,
                                       IsActive = x.IsActive,
                                       Id = x.Id
                                   }).ToList()
                               }).FirstOrDefaultAsync().ConfigureAwait(false);
            return query ?? new GetDiscountPriceSettingsResponse
            {
                FacilityId = facilityId,
                GlobalDiscount = 0,
                PriceCategoryDiscounts = new List<PriceCategoryDiscountResponse>()
            };
        }
    }
}
