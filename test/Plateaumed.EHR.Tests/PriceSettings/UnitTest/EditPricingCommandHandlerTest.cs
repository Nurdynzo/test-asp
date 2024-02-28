using System.Threading.Tasks;
using Abp.Domain.Repositories;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Command;
using Plateaumed.EHR.PriceSettings.Dto;
using Xunit;
namespace Plateaumed.EHR.Tests.PriceSettings.UnitTest
{
    [Trait("Category","Unit")]
    public class EditPricingCommandHandlerTest
    {
        private readonly IRepository<ItemPricing, long> _itemPricingRepositoryMock
            = Substitute.For<IRepository<ItemPricing, long>>();

        [Fact]
        public async Task Handle_Edit_Pricing_Should_Update()
        {
            //Arrange
            _itemPricingRepositoryMock.GetAsync(Arg.Any<long>()).Returns(new ItemPricing());
            var handler = new EditPricingCommandHandler(_itemPricingRepositoryMock);
            //Act
            var request = new EditPricingCommandRequest();
            await handler.Handle(request);
            //Assert
            await _itemPricingRepositoryMock.Received(1).UpdateAsync(Arg.Any<ItemPricing>());
        }
    }
}
