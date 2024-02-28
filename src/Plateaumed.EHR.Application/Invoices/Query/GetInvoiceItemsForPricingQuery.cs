using System.Threading.Tasks;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Query;

/// <inheritdoc />
public class GetInvoiceItemsForPricingQuery : IGetInvoiceItemsForPricingQuery
{
     private readonly IRepository<ItemPricing, long> _itemPricingRepository;
     private readonly IRepository<PriceCategoryDiscount ,long> _priceCategoryDiscountRepository;
     private readonly IRepository<PricingDiscountSetting,long> _pricingSettingRepository;

     /// <summary>
     /// constructor for GetInvoiceItemsForPricingQuery
     /// </summary>
     /// <param name="itemPricingRepository"></param>
     /// <param name="itemPricingCategoryRepository"></param>
     /// <param name="pricingSettingRepository"></param>
     public GetInvoiceItemsForPricingQuery(
         IRepository<ItemPricing, long> itemPricingRepository, 
         IRepository<PriceCategoryDiscount, long> itemPricingCategoryRepository,
         IRepository<PricingDiscountSetting, long> pricingSettingRepository)
     {
         _itemPricingRepository = itemPricingRepository;
         _priceCategoryDiscountRepository = itemPricingCategoryRepository;
         _pricingSettingRepository = pricingSettingRepository;
     }

     public async Task<PagedResultDto<GetInvoiceItemPricingResponse>> Handle(GetInvoiceItemPricingRequest request, long facilityId)
     {
         var filter = string.IsNullOrEmpty(request.Filter) ? "" : request.Filter.ToLower();
         var category =
             from d in _priceCategoryDiscountRepository.GetAll()
             join g in _pricingSettingRepository.GetAll() on d.PricingDiscountSettingId equals g.Id
             select new
             {
                 d.PricingCategory,
                 d.Percentage,
                 g.GlobalDiscount,
                 d.IsActive,
                 d.FacilityId
             };
         var query =
             from i in _itemPricingRepository.GetAll().AsNoTracking()
             let dc = category.FirstOrDefault(x
                 => x.PricingCategory == i.PricingCategory
                    && x.PricingCategory == PricingCategory.Consultation
                    && x.IsActive && x.FacilityId == facilityId)
             where i.FacilityId == facilityId
                   && (filter == "" || i.Name.ToLower().Contains(filter))
                   && i.PricingCategory == PricingCategory.Consultation
                   && i.IsActive
             orderby i.Name
             select new GetInvoiceItemPricingResponse
             {
                 Id = i.Id,
                 Name = $"{i.PricingCategory}: {i.Name}",
                 DiscountName = dc != null && dc.Percentage > 0
                     ? $"(Category: {dc.Percentage}%)" : dc != null && dc.GlobalDiscount > 0 ? $"(Global: {dc.GlobalDiscount}%)": "",
                 Amount = i.Amount.ToMoneyDto(),
                 DiscountPercentage = dc != null && dc.Percentage > 0 ? dc.Percentage : ( dc != null ? dc.GlobalDiscount: 0),
                 IsGlobal = dc != null && dc.GlobalDiscount <= 0
             };

         int count = await query.CountAsync().ConfigureAwait(false);
         var result = await query.ToListAsync().ConfigureAwait(false);

         return new PagedResultDto<GetInvoiceItemPricingResponse>(count, result);
     }
}
