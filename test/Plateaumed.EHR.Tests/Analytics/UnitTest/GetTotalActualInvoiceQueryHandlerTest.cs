using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Analytics;
using Plateaumed.EHR.Analytics.Query;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using Plateaumed.EHR.Tests.Analytics.Util;
using Shouldly;
using System;
using Xunit;

namespace Plateaumed.EHR.Tests.Analytics.UnitTest
{
    public class GetTotalActualInvoiceQueryHandlerTest
    {
        private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();

        [Fact]
        public void GetTotalActualInvoiceQueryHandlerShould_Get_The_Desired_Invoices_In_Time_Range()
        {
            //Arrange
            _invoiceRepository.GetAll().Returns(AnalyticsCommon.GetInvoicForAnalyticsAsQueryable().BuildMock());
            var handler = new GetTotalActualInvoiceQueryHandler(_invoiceRepository);
            //Act
            var result = handler.Handle(1, AnalyticsEnum.Today);
            //Assert
            result.Result.ShouldBeOfType<GetAnalyticsResponseDto>();
        }

       [Fact]
        public void GetTotalActualInvoiceQueryHandlerShould_CalculateCorrectly()
        {
            //Arrange
            _invoiceRepository.GetAll().Returns(AnalyticsCommon.GetInvoicForAnalyticsAsQueryable().BuildMock());
            var handler = new GetTotalActualInvoiceQueryHandler(_invoiceRepository);
            //Act
            var result = handler.Handle(1, AnalyticsEnum.ThisWeek);
            //Assert
            result.Result.Total.Amount.ShouldBeGreaterThanOrEqualTo(10000);
            result.Result.PercetageIncreaseOrDecrease.ShouldBeLessThan(0);
            result.Result.Increased.ShouldBeFalse();
        }
    }
}
