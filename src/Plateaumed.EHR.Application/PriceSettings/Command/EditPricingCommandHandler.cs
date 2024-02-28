using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.Utility;
namespace Plateaumed.EHR.PriceSettings.Command
{
    public class EditPricingCommandHandler : IEditPricingCommandHandler
    {
        private readonly IRepository<ItemPricing, long> _itemPricingRepository;
        public EditPricingCommandHandler(IRepository<ItemPricing, long> itemPricingRepository)
        {
            _itemPricingRepository = itemPricingRepository;
        }

        public async Task Handle(EditPricingCommandRequest request)
        {
            var itemToEdit = await _itemPricingRepository.GetAsync(request.Id)
                ?? throw new UserFriendlyException($"Pricing Item with id {request.Id} not found");
            itemToEdit.PricingCategory = request.PricingCategory;
            itemToEdit.Amount = request.Amount.ToMoney();
            itemToEdit.ItemId = request.ItemId;
            itemToEdit.Name = request.Name;
            itemToEdit.PricingType = request.PricingType;
            itemToEdit.SubCategory = request.SubCategory;
            itemToEdit.Frequency = request.Frequency;
            await _itemPricingRepository.UpdateAsync(itemToEdit);
        }
    }
}
