using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.PriceSettings.Command;
using Plateaumed.EHR.PriceSettings.Dto;
using Xunit;

namespace Plateaumed.EHR.Tests.PriceSettings.UnitTest
{
    [Trait("Category", "Unit")]
    public class EditInvestigationPricingCommandHandlerTest : AppTestBase
    {
        private readonly IRepository<InvestigationPricing, long> _investigationPricingRepositoryMock
           = Substitute.For<IRepository<InvestigationPricing, long>>();

        private readonly IRepository<Investigation, long> _investigationMock
            = Substitute.For<IRepository<Investigation, long>>();

        private readonly IAbpSession _abpSession;

        public EditInvestigationPricingCommandHandlerTest()
        {
            _abpSession = Resolve<IAbpSession>();
        }

        [Fact]
        public async Task HandleEditInvestigationPricingShouldUpdate()
        {

            LoginAsDefaultTenantAdmin();
            //Arrange
            _investigationPricingRepositoryMock.GetAsync(Arg.Any<long>()).Returns(new InvestigationPricing());
            _investigationMock.GetAsync(Arg.Any<long>()).Returns(new Investigation());
            
            var handler = new UpdateInvestigationPricingCommandHandler(_investigationPricingRepositoryMock, _abpSession, _investigationMock);
            //Act
            var request = new UpdateInvestigationPricingRequestCommand()
            {
                InvestigationId = 1,
                InvestigationPricingId = 1,
                Notes = "This note was modified",
                IsActive = true
            };
            await handler.Handle(request);
            //Assert
            await _investigationPricingRepositoryMock.Received(1).UpdateAsync(Arg.Any<InvestigationPricing>());
        }
    }

}

