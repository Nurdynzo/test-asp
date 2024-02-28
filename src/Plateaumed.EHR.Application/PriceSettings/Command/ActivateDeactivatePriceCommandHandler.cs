using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Command
{
    public class ActivateDeactivatePriceCommandHandler : IActivateDeactivatePriceCommandHandler
    {
         private readonly IRepository<ItemPricing, long> _itemPricingRepository;
         public ActivateDeactivatePriceCommandHandler(IRepository<ItemPricing, long> itemPricingRepository)
         {
             _itemPricingRepository = itemPricingRepository;
         }

         public async Task Handle(ActivateDeactivatePriceCommandRequest request)
         {
             var priceItem = await _itemPricingRepository.GetAsync(request.Id);
             priceItem.IsActive = request.IsActive;
             await _itemPricingRepository.UpdateAsync(priceItem);
         }
    }
}
