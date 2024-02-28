using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Analytics;
using Plateaumed.EHR.Analytics.Query;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using Plateaumed.EHR.Tests.Analytics.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Analytics.UnitTest
{
    public class TotalWalletTopupQueryHandlerTest
    {
        private readonly IRepository<PaymentActivityLog, long> _logRepository = Substitute.For<IRepository<PaymentActivityLog, long>>();

        [Fact]
        public void TotalWalletTopupQueryHandlerTest_Should_Produce_Desired_Result()
        {
            //Arrange
            _logRepository.GetAll().Returns(AnalyticsCommon.GetLogActivitiesForAnalyticsAsQueryable().BuildMock());
            var handler = new TotalWalletTopUpQueryHandler(_logRepository);
            //Act
            var result = handler.Handle(1, AnalyticsEnum.Today);
            //Assert
            result.Result.ShouldBeOfType<GetAnalyticsResponseDto>();
        }

        [Fact]
        public void TotalWalletTopupQueryHandlerTest_Calculate_Correctly()
        {
            //Arrange
            _logRepository.GetAll().Returns(AnalyticsCommon.GetLogActivitiesForAnalyticsAsQueryable().BuildMock());
            var handler = new TotalWalletTopUpQueryHandler(_logRepository);
            //Act
            var result = handler.Handle(1, AnalyticsEnum.Today);
            //Assert
            result.Result.Total.Amount.ShouldBeGreaterThanOrEqualTo(700);
            result.Result.PercetageIncreaseOrDecrease.ShouldBeGreaterThan(0);
            result.Result.Increased.ShouldBeTrue();
        }
    }
}
