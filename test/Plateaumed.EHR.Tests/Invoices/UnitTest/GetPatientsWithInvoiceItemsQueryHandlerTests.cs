using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Tests.Invoices.Util;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet;
using System.Linq;
using MockQueryable.NSubstitute;
using System;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices.Dtos.PatientWithInvoiceItemsDtos;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetPatientsWithInvoiceItemsQueryHandlerTests
    {

        private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository = Substitute.For<IRepository<InvoiceItem, long>>();
        private readonly IRepository<Patient, long> _patientRepository = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepository = Substitute.For<IRepository<PatientCodeMapping, long>>();
        private readonly IRepository<Wallet, long> _walletRepository = Substitute.For<IRepository<Wallet, long>>();
        private readonly IRepository<PatientAppointment,long> _patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();


        [Fact]
        public async Task Handle_Given_Request_With_Filter_Name_Should_Return_Invoices_With_Filter_Parameter()
        {
            // Arrange
            var request = new PatientsWithInvoiceItemsRequest { Filter = "Sam" };
            MockDependencies();
            var handler = GetHandler();
            // act
            var response = await handler.Handle(request, facilityId: 1);
            // Assert
            response.TotalCount.ShouldBe(1);
            response.Items.ShouldNotBeEmpty();
            response.Items[0].FirstName.ShouldBe("Sam");
            response.Items[0].LastName.ShouldBe("Ade");
        }



        [Fact]
        public async Task Handle_GivenPatientHasNoUnpaidInvoiceOnAParticularDate_ShouldReturnEmptyList()
        {

            // Arrange
            var request = new PatientsWithInvoiceItemsRequest { StartDate = DateTime.Now.AddMonths(-4) };
            MockDependencies();
            var handler = GetHandler();

            // Act
            var response = await handler.Handle(request, facilityId: 1);

            // Assert
            response.TotalCount.ShouldBe(0);
            response.Items.ShouldBeEmpty();
        }

        [Fact]
        public async Task Handle_GivenPatientHasUnpaidInvoiceItemsInitiatedToday_ShouldReturnPatientsWithUnpaidInvoiceItemsForToday()
        {

            // Arrange
            var request = new PatientsWithInvoiceItemsRequest();
            MockDependencies();
            var handler = GetHandler();

            // Act
            var response = await handler.Handle(request, facilityId: 1);

            // Assert
            response.TotalCount.ShouldBe(1);
            response.Items.ShouldNotBeEmpty();


            response.Items[0].FirstName.ShouldBe("Sam");
            response.Items[0].LastName.ShouldBe("Ade");
            response.Items[0].PatientCode.ShouldBe("000001");
            response.Items[0].DateOfBirth.ShouldBe(DateTime.Now.AddYears(-20).Date);
            response.Items[0].GenderType.ShouldBe(GenderType.Male);
            response.Items[0].WalletBalance.Amount.ShouldBe(0.00M);

            response.Items[0].TotalOutstanding.Amount.ShouldBe(100);

            response.Items[0].HasPendingWalletFundingRequest.ShouldBe(false);

            response.Items[0].LastPaymentDate.ShouldBe(DateTime.Today);

            response.Items[0].InvoiceItems.Count().ShouldBe(3);
            response.Items[0].InvoiceItems.First().Id.ShouldBe(3);
            response.Items[0].InvoiceItems.Count(x => x.Status == InvoiceItemStatus.Unpaid.ToString()).ShouldBe(2);
            response.Items[0].InvoiceItems.Count(x => x.Status == InvoiceItemStatus.Paid.ToString()).ShouldBe(1);
        }

        private GetPatientsWithInvoiceItemsQueryHandler GetHandler()
        {

            var handler = new GetPatientsWithInvoiceItemsQueryHandler(_patientRepository, _patientCodeMappingRepository, 
                _invoiceRepository, _invoiceItemRepository, _walletRepository,_patientAppointmentRepository);
            return handler;
        }

        private void MockDependencies() {

            _invoiceRepository.GetAll().Returns(GetPatientsWithInvoiceItemsTestData.GetInvoiceAsQueryable().BuildMock());
            _invoiceItemRepository.GetAll().Returns(GetPatientsWithInvoiceItemsTestData.GetInvoiceItemAsQueryable().BuildMock());
            _walletRepository.GetAll().Returns(GetPatientsWithInvoiceItemsTestData.GetWallets().BuildMock());
            _patientCodeMappingRepository.GetAll().Returns(GetPatientsWithInvoiceItemsTestData.GetPatientCodeMappings().BuildMock());
            _patientRepository.GetAll().Returns(GetPatientsWithInvoiceItemsTestData.GetPatients().BuildMock());
            _patientAppointmentRepository.GetAll().Returns(GetPatientsWithInvoiceItemsTestData.GetPatientAppointments().BuildMock());
        }

    }
}
