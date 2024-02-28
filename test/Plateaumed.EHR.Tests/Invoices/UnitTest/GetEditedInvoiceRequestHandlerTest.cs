using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetEditedInvoiceRequestHandlerTest
    {
        private readonly IRepository<PaymentActivityLog, long> _logRepository = Substitute.For<IRepository<PaymentActivityLog, long>>();   
        private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<Patient, long> _patientRepository = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepositoryMock = Substitute.For<IRepository<PatientCodeMapping, long>>();

        [Fact]
        public async Task GetEditedInvoiceRequestHandler_Should_Return_List_Of_Edited_Invoices()
        {
            //Arrange
           var request = new GetEditedInvoiceRequestDto 
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(1),
                FilterDate = PatientSeenFilter.Today,
            };
            _logRepository.GetAll().Returns(CommonUtil.GetPaymentActivityLogForEditedInvoiceAsQueryable().BuildMock());
            _invoiceRepository.GetAll().Returns(CommonUtil.GetInvoiceAsQueryable().BuildMock());
            _patientRepository.GetAll().Returns(CommonUtil.GetPatientList().BuildMock());
            _patientCodeMappingRepositoryMock.GetAll().Returns(CommonUtil.GetPatientCodeMappingList().BuildMock());
            var handler = new GetEditedInvoiceQueryHandler(
                _logRepository,
                _patientRepository,
                _patientCodeMappingRepositoryMock,
                _invoiceRepository);
            //Act
            var result = await handler.Handle(request,1);
            //Assert
            result.Items.ShouldNotBeNull();
            result.Items.Count.ShouldBe(4);
        }
    }
}
