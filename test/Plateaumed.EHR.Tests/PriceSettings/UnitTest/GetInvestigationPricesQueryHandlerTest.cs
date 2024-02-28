using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.PriceSettings.Query;
using Plateaumed.EHR.Utility;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PriceSettings.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetInvestigationPricesQueryHandlerTest
    {
        private readonly IRepository<InvestigationPricing, long> _investigationPricing = Substitute.For<IRepository<InvestigationPricing, long>>();

        [Fact]
        public async Task HandleGetInvestigationPricesShouldReturnAllItems()
        {
            //Arrange
            _investigationPricing.GetAll().Returns(GetAllPriceList().BuildMock());
            var handler = new GetInvestigationPricesQueryHandler(_investigationPricing);
            var request = new GetInvestigationPricingRequestDto
            {
                InvestigationType = "",
                TestName = "",
                SortBy =""
            };
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.TotalCount.ShouldBe(3);
            result.Items.Count.ShouldBe(3);
        }

        [Fact]
        public async Task GetInvestigationPricesShouldReturnItemWithInvestigationTypeFilter()
        {
            //Arrange
            _investigationPricing.GetAll().Returns(GetAllPriceList().BuildMock());
            var handler = new GetInvestigationPricesQueryHandler(_investigationPricing);
            var request = new GetInvestigationPricingRequestDto
            {
                InvestigationType = "Microbiology",
                TestName = "",
                SortBy = ""
            };
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.TotalCount.ShouldBe(1);
            result.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GetInvestigationPricesShouldReturnItemWithTestNameFilter()
        {   
            _investigationPricing.GetAll().Returns(GetAllPriceList().BuildMock());
            var handler = new GetInvestigationPricesQueryHandler(_investigationPricing);
            var request = new GetInvestigationPricingRequestDto
            {
                InvestigationType = "",
                TestName = "Blood Culture",
                SortBy = ""
            };
           
            var result = await handler.Handle(request);
           
            result.TotalCount.ShouldBe(1);
            result.Items.Count.ShouldBe(1);
        }

        private IQueryable<InvestigationPricing> GetAllPriceList()
        {
            return new List<InvestigationPricing>
            {
                new()
                {
                    Id = 1,
                    InvestigationId = 1,
                    Amount = 1000.00m.ToMoney("NGN"),
                    IsActive = true,
                    TenantId = 1,
                    Notes = "Test Investigation Price",
                    CreationTime = DateTime.Now,
                    Investigation = GetInvestigations().Where(x=>x.Id == 1).FirstOrDefault()
                },
                new()
                {
                    Id = 2,
                    InvestigationId = 2,
                    Amount = 100.00m.ToMoney("NGN"),
                    IsActive = true,
                    TenantId = 1,
                    Notes = "Test Investigation Price2",
                    CreationTime = DateTime.Now,
                    Investigation = GetInvestigations().Where(x=>x.Id == 2).FirstOrDefault()
                },
                new()
                {
                    Id = 3,
                    InvestigationId = 1,
                    Amount = 2000.00m.ToMoney("NGN"),
                    IsActive = true,
                    TenantId = 1,
                    Notes = "Test Investigation Price3",
                    CreationTime = DateTime.Now,
                    Investigation = GetInvestigations().Where(x=>x.Id == 3).FirstOrDefault()
                }
            }.AsQueryable();
        }

        private IQueryable<Investigation> GetInvestigations()
        {
            return new List<Investigation>
            {
                new()
                {
                    Id = 1,
                    Name = "Blood Culture",
                    Type = "Microbiology"
                },
                new()
                {
                    Id = 2,
                    Name = "Full Blood Count (FBC)",
                    Type = "Haematology"
                },
                new()
                {
                    Id = 3,
                    Name = "Electrolytes",
                    Type = "Chemistry"
                }
            }.AsQueryable();
        }
    }
}

