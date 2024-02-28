using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetInvestigationPricesCommandHandlerTests
	{
        private readonly IRepository<InvestigationPricing, long> _investigationPricing = Substitute.For<IRepository<InvestigationPricing, long>>();

        [Fact]
        public async Task HandleGivenInvestigationIdsShouldReturnInvestigationsAndPrices()
        {
            var Ids = new List<long> { 1, 2 };
            
            var request = new GetInvestigationPricesRequest { InvestigationIds = Ids };

            _investigationPricing.GetAll().Returns(GetInvestigationPricing().BuildMock());

            var handler = new GetInvestigationPricesRequestCommandHandler(_investigationPricing);

            var result = await handler.GetInvestigationPrice(request);

            result.ShouldNotBeNull();
            result.InvestigationsAndPrices.ShouldNotBeNull();
            result.InvestigationsAndPrices.Count.ShouldBe(2);
        }

        [Fact]
        public async Task HandleGivenInvalidInvestigationIdsShouldReturnZeroCount()
        {
            var Ids = new List<long> {0 };

            var request = new GetInvestigationPricesRequest { InvestigationIds = Ids };

            _investigationPricing.GetAll().Returns(GetInvestigationPricing().BuildMock());

            var handler = new GetInvestigationPricesRequestCommandHandler(_investigationPricing);

            var result = await handler.GetInvestigationPrice(request);

            result.ShouldNotBeNull();
            result.InvestigationsAndPrices.ShouldNotBeNull();
            result.InvestigationsAndPrices.Count.ShouldBe(0);
        }

        [Fact]
        public async Task HandleGivenNoInvestigationIdsShouldThrowAnError()
        {
            var Ids = new List<long> { };

            var request = new GetInvestigationPricesRequest { InvestigationIds = Ids };

            _investigationPricing.GetAll().Returns(GetInvestigationPricing().BuildMock());

            var handler = new GetInvestigationPricesRequestCommandHandler(_investigationPricing);

            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.GetInvestigationPrice(request));
           
            exception.Message.ShouldBe("Investigations not found");
        }


        private static IQueryable<InvestigationPricing> GetInvestigationPricing()
        {
            return new List<InvestigationPricing>
            {
                new()
                {
                    Amount = new ValueObjects.Money { Amount = 100, Currency = "USD"},
                    Id = 1,
                    Investigation = new Investigation
                    {
                        Name = "Electrolyte"
                    },
                    InvestigationId = 1
                },
                new()
                {
                    Amount = new ValueObjects.Money { Amount = 200, Currency = "USD"},
                    Id = 2,
                    Investigation = new Investigation
                    {
                        Name = "Urinalysis"
                    },
                    InvestigationId = 2
                }
            }.AsQueryable();
        }
    }
}

