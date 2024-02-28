using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Analytics;
using Plateaumed.EHR.Analytics.Query;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Tests.Analytics.Util;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Analytics.UnitTest
{
    public class AnalyticsForCancelledVsUncancelledInvoiceQueryHandlerTest
    {
        private readonly IRepository<InvoiceCancelRequest, long> _logRepository = Substitute.For<IRepository<InvoiceCancelRequest, long>>();

        [Fact]
        public async Task AnalyticsForCancelledVsUncancelledInvoiceQueryHandler_ShouldCalculateCorrectly()
        {
            //Arrange
            _logRepository.GetAll().Returns(AnalyticsCommon.GetInvoicesCancelForAnalyticsAsQueryable().BuildMock());
            var handler = new AnalyticsForCancelledVsUncancelledInvoiceQueryHandler(_logRepository);
            //Act
            var result = await handler.Handle(1, AnalyticsEnum.Today);
            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
        }
    }
}
