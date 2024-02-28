using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.Utility;
namespace Plateaumed.EHR.PriceSettings.Query
{
    public class GetPriceListQueryHandler : IGetPriceListQueryHandler
    {
        private readonly IRepository<ItemPricing,long> _itemPricingRepository;
        public GetPriceListQueryHandler(IRepository<ItemPricing, long> itemPricingRepository)
        {
            _itemPricingRepository = itemPricingRepository;
        }

        public async Task<PagedResultDto<GetPriceListQueryResponse>> Handle(GetPriceListQueryRequest request)
        {
            var filter = !string.IsNullOrEmpty(request.SearchText) ? request.SearchText.ToLower() : "";
            var query =
                (from p in _itemPricingRepository.GetAll().AsNoTracking()
                 where p.FacilityId == request.FacilityId
                       && (p.Name.ToLower().Contains(filter) || p.ItemId.ToLower().Contains(filter))
                       && (request.PricingType == null || p.PricingType == request.PricingType)
                 orderby p.CreationTime descending
                 select new GetPriceListQueryResponse
                 {
                     Id = p.Id,
                     ItemId = p.ItemId,
                     Name = p.Name,
                     PricingCategory = p.PricingCategory,
                     Price = p.Amount.ToMoneyDto(),
                     PricingType = p.PricingType,
                     IsActive = p.IsActive,
                     SubCategory = p.SubCategory,
                     Frequency = p.Frequency
                 });
            //filter
            query = request switch
            {
                { IsActive: true, PricingCategory: PricingCategory.Consultation } => query.Where(x => x.IsActive && x.PricingCategory == PricingCategory.Consultation),
                { IsActive: true, PricingCategory: PricingCategory.Laboratory } => query.Where(x => x.IsActive && x.PricingCategory == PricingCategory.Laboratory),
                { IsActive: true, PricingCategory: PricingCategory.Others } => query.Where(x => x.IsActive && x.PricingCategory == PricingCategory.Others),
                { IsActive: true, PricingCategory: PricingCategory.Procedure } => query.Where(x => x.IsActive && x.PricingCategory == PricingCategory.Procedure),
                { IsActive: true, PricingCategory: PricingCategory.WardAdmission } => query.Where(x => x.IsActive && x.PricingCategory == PricingCategory.WardAdmission),
                { IsActive: false, PricingCategory: PricingCategory.Consultation } => query.Where(x => !x.IsActive && x.PricingCategory == PricingCategory.Consultation),
                { IsActive: false, PricingCategory: PricingCategory.Laboratory } => query.Where(x => !x.IsActive && x.PricingCategory == PricingCategory.Laboratory),
                { IsActive: false, PricingCategory: PricingCategory.Others } => query.Where(x => !x.IsActive && x.PricingCategory == PricingCategory.Others),
                { IsActive: false, PricingCategory: PricingCategory.Procedure } => query.Where(x => !x.IsActive && x.PricingCategory == PricingCategory.Procedure),
                { IsActive: false, PricingCategory: PricingCategory.WardAdmission } => query.Where(x => !x.IsActive && x.PricingCategory == PricingCategory.WardAdmission),
                { IsActive: false } => query.Where(x => !x.IsActive),
                { IsActive: true } => query.Where(x => x.IsActive),
                { PricingCategory: not null } => query.Where(x => x.PricingCategory == request.PricingCategory),
                _ => query

            };
            var count = await query.CountAsync().ConfigureAwait(false);
            var items = await query.Skip(request.SkipCount).Take(request.MaxResultCount).ToListAsync().ConfigureAwait(false);
            return new PagedResultDto<GetPriceListQueryResponse>(count, items);
        }
    }
}
