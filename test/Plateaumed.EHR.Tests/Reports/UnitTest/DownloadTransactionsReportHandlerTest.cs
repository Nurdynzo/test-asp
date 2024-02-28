using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Dto;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Reports.PaymentTransactions.Exporting;
using Plateaumed.EHR.Reports.PaymentTransactions.Query;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Reports.UnitTest
{
    [Trait("Category", "Unit")]
    public class DownloadTransactionsReportHandlerTest
    {
        private readonly IRepository<Patient, long> _patientRepository = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogMock =
            Substitute.For<IRepository<PaymentActivityLog, long>>();
        private readonly ITransactionsExporter _transactionExporter = Substitute.For<ITransactionsExporter>();
        private readonly IRepository<Facility, long> _facilityRepository = Substitute.For<IRepository<Facility, long>>();


        [Fact]
        public async Task Handle_Should_Return_PaymentSummary_For_The_Given_Patient()
        {
            // arrange 
            var request = new DownloadPaymentActivityRequest
            {
                StartTime = DateTime.UtcNow.AddDays(-2),
                EndTime = DateTime.UtcNow.AddDays(2)
            };
            _paymentActivityLogMock.GetAll().Returns(CommonUtil.GetPaymentActivityLogAsQueryable().BuildMock());
            _patientRepository.GetAll().Returns(CommonUtil.GetPatientList().BuildMock());
            _facilityRepository.GetAll().Returns(GetCurrentFacility().BuildMock());
            var handler = new DownloadPaymentTransactionQueryHelper(_paymentActivityLogMock, _patientRepository,
                _transactionExporter, _facilityRepository);

            //act
            var result = await handler.Handle(request, 1);

            // assert   
            result.ShouldNotBeOfType<FileDto>();
        }

        public static IQueryable<Facility> GetCurrentFacility()
        {
            return new List<Facility>
        {
            new Facility
            {
                Id = 1,
                Name = "Test 1 Tenant"
            },
            new Facility
            {
                Id = 2,
                Name = "Test 2 Tenant"
            }
        }.AsQueryable();
        }
    }
}
