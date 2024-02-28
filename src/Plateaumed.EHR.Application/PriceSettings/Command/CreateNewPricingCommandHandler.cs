using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Command
{
    public class CreateNewPricingCommandHandler : ICreateNewPricingCommandHandler
    {
        private readonly IRepository<ItemPricing,long> _itemPricingRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;
        public CreateNewPricingCommandHandler(
            IObjectMapper objectMapper, 
            IRepository<ItemPricing, long> itemPricingRepository,
            IAbpSession abpSession)
        {
            _objectMapper = objectMapper;
            _itemPricingRepository = itemPricingRepository;
            _abpSession = abpSession;
        }
        public async Task Handle(List<CreateNewPricingCommandRequest> request)
        {
          var itemPricing =  _objectMapper.Map<List<ItemPricing>>(request);
          foreach (var item in itemPricing)
          {
              item.IsActive = true;
              item.TenantId = _abpSession.GetTenantId();
          }
          await _itemPricingRepository.InsertRangeAsync(itemPricing);
        }
    }
}
