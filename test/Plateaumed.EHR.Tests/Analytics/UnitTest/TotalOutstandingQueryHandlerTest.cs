using Abp.Domain.Repositories;
using Plateaumed.EHR.Analytics.Query;
using Plateaumed.EHR.Analytics;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Tests.Analytics.Util;
using Xunit;
using NSubstitute;
using MockQueryable.NSubstitute;
using Shouldly;

namespace Plateaumed.EHR.Tests.Analytics.UnitTest
{
    public class TotalOutstandingQueryHandlerTest
    {
        private readonly IRepository<PaymentActivityLog, long> _logRepository = Substitute.For<IRepository<PaymentActivityLog, long>>();

        [Fact]
        public void GetTotalOutstandingQueryHandlerTest_Should_Produce_Desired_Result()
        {
            //Arrange
            _logRepository.GetAll().Returns(AnalyticsCommon.GetLogActivitiesForAnalyticsAsQueryable().BuildMock());
            var handler = new GetTotalOutstandingQueryHandler(_logRepository);
            //Act
            var result = handler.Handle(1, AnalyticsEnum.Today);
            //Assert
            result.Result.ShouldBeOfType<GetAnalyticsResponseDto>();
        }

        [Fact]
        public void GetTotalOutstandingQueryHandlerTest_Calculate_Correctly()
        {
            //Arrange
            _logRepository.GetAll().Returns(AnalyticsCommon.GetLogActivitiesForAnalyticsAsQueryable().BuildMock());
            var handler = new GetTotalOutstandingQueryHandler(_logRepository);
            //Act
            var result = handler.Handle(1, AnalyticsEnum.Today);
            //Assert
            result.Result.Total.Amount.ShouldBeGreaterThanOrEqualTo(200);
            result.Result.PercetageIncreaseOrDecrease.ShouldBe(0);
            result.Result.Increased.ShouldBeFalse();
        }
    }
}
