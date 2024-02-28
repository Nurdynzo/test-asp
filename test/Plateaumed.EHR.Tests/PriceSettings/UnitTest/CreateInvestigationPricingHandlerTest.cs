using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Command;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.PriceSettings.Dto;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PriceSettings.UnitTest
{
    [Trait("Category", "Unit")]
    public class CreateInvestigationPricingHandlerTest: AppTestBase
    {
        private readonly IRepository<InvestigationPricing, long> _investigationPricingRepositoryMock
           = Substitute.For<IRepository<InvestigationPricing, long>>();

        private readonly IAbpSession _abpSession;
        private readonly IObjectMapper _mapper;
        public CreateInvestigationPricingHandlerTest()
        {
            _abpSession = Resolve<IAbpSession>();
            _mapper = Resolve<IObjectMapper>();
        }

        [Fact]
        public async Task HandleShouldThrowIfGivenInvalidTenantId()
        {
            LoginAsHostAdmin();
            _investigationPricingRepositoryMock.GetAsync(Arg.Any<long>()).Returns(new InvestigationPricing());

            var request = new List<CreateInvestigationPricingDto>()
            {
                new CreateInvestigationPricingDto
                {
                    Amount = new MoneyDto { Amount = 10000 },
                    InvestigationId = 1,
                    Notes = "Unit test",
                    IsActive = false
                },
                new CreateInvestigationPricingDto
                {
                    Amount = new MoneyDto { Amount = 20000 },
                    InvestigationId = 1,
                    Notes = "Unit test2",
                    IsActive = false
                }
            };
            var handler = new CreateInvestigationPricingCommandHandler(_investigationPricingRepositoryMock, _abpSession, _mapper);

            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            exception.Message.ShouldBe("Tenant not found");
        }


        [Fact]
        public async Task HandleShouldThrowIfGivenInvalidInvestigation()
        {
             LoginAsDefaultTenantAdmin();
            _investigationPricingRepositoryMock.GetAsync(Arg.Any<long>()).Returns(new InvestigationPricing());

            var request = new List<CreateInvestigationPricingDto>()
            {
                new CreateInvestigationPricingDto
                {
                    Amount = new MoneyDto { Amount = 10000 },
                    InvestigationId = 0,
                    Notes = "Unit test",
                    IsActive = false
                },
                new CreateInvestigationPricingDto
                {
                    Amount = new MoneyDto { Amount = 20000 },
                    InvestigationId = 0,
                    Notes = "Unit test2",
                    IsActive = false
                }
            };

            var handler = new CreateInvestigationPricingCommandHandler(_investigationPricingRepositoryMock, _abpSession, _mapper);

            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));

            exception.Message.ShouldBe("Investigation not found");
        }

        
    }
}
