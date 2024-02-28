using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.PriceSettings.Query;
using Plateaumed.EHR.Utility;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.PriceSettings.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetPriceListQueryHandlerTest
    {
        private readonly IRepository<ItemPricing,long> _itemPricingRepositoryMock
            = Substitute.For<IRepository<ItemPricing, long>>();

        [Fact]
        public async Task Handle_Get_Price_List_With_Filter_Should_Return_All_List()
        {
            //Arrange
            _itemPricingRepositoryMock.GetAll().Returns(GetAllPriceList().BuildMock());
            var handler = new GetPriceListQueryHandler(_itemPricingRepositoryMock);
            var request = new GetPriceListQueryRequest
            {
                FacilityId = 1,
            };
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.TotalCount.ShouldBe(3);
            result.Items.Count.ShouldBe(3);
        }
        [Fact]
        public async Task Handle_Get_Price_List_With_Active_Filter_Should_Return_All_Active_List()
        {
            //Arrange
            _itemPricingRepositoryMock.GetAll().Returns(GetAllPriceList().BuildMock());
            var handler = new GetPriceListQueryHandler(_itemPricingRepositoryMock);
            var request = new GetPriceListQueryRequest
            {
                FacilityId = 1,
                IsActive = true,
            };
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);

        }
        [Fact]
        public async Task Handle_Get_Price_List_With_InActive_Filter_Should_Return_All_InActive_List()
        {
            //Arrange
            _itemPricingRepositoryMock.GetAll().Returns(GetAllPriceList().BuildMock());
            var handler = new GetPriceListQueryHandler(_itemPricingRepositoryMock);
            var request = new GetPriceListQueryRequest
            {
                FacilityId = 1,
                IsActive = false,
            };
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.TotalCount.ShouldBe(1);
            result.Items.Count.ShouldBe(1);
        }
        [Fact]
        public async Task Handle_Get_Price_List_With_Search_Filter_Should_Return_Searched_List()
        {
            //Arrange
            _itemPricingRepositoryMock.GetAll().Returns(GetAllPriceList().BuildMock());
            var handler = new GetPriceListQueryHandler(_itemPricingRepositoryMock);
            var request = new GetPriceListQueryRequest
            {
                FacilityId = 1,
                SearchText = "Item1"
            };
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.TotalCount.ShouldBe(1);
            result.Items.Count.ShouldBe(1);

        }
        private IQueryable<ItemPricing> GetAllPriceList()
        {
            return new List<ItemPricing>
            {
                new()
                {
                    Id = 1,
                    FacilityId = 1,
                    ItemId = "Item1",
                    Name = "Item1",
                    PricingCategory = PricingCategory.Consultation,
                    Amount = 1000.00m.ToMoney("NGN"),
                    PricingType = PricingType.GeneralPricing,
                    IsActive = true,
                    TenantId = 1,
                },
                new()
                {
                    Id = 2,
                    FacilityId = 1,
                    ItemId = "Item2",
                    Name = "Item2",
                    PricingCategory = PricingCategory.Consultation,
                    Amount = 1000.00m.ToMoney("NGN"),
                    PricingType = PricingType.GeneralPricing,
                    IsActive = true,
                    TenantId = 1,
                },
                new()
                {
                    Id = 3,
                    FacilityId = 1,
                    ItemId = "Item3",
                    Name = "Item3",
                    PricingCategory = PricingCategory.Laboratory,
                    Amount = 1000.00m.ToMoney("NGN"),
                    PricingType = PricingType.GeneralPricing,
                    IsActive = false,
                    TenantId = 1,

                }
            }.AsQueryable();
        }
    }
}
