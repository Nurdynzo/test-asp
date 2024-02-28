using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.PatientInvoicesAndWalletTransactionsDtos;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetPatientInvoicesAndWalletTransactionsQueryHandlerTests
    {
        private readonly IRepository<User, long> _userRepository = Substitute.For<IRepository<User, long>>();
        private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository = Substitute.For<IRepository<InvoiceItem, long>>();
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogRepository = Substitute.For<IRepository<PaymentActivityLog, long>>();

        [Fact]
        public async Task Handle_GivenPatientHasNoPaymentActivity_ShouldReturnEmptyList()
        {

            // Arrange
            var request = new PatientInvoicesAndWalletTransactionsRequest { PatientId = 2 };
            MockDependencies();
            var handler = GetHandler();

            // Act
            var response = await handler.Handle(request, facilityId: 1);

            // Assert
            response.TotalCount.ShouldBe(0);
            response.Items.ShouldBeEmpty();
        }


        [Fact]
        public async Task Handle_GivenCarriedOutPaymentActivities_ShouldReturnAllPaymentActivitiesCarriedOut()
        {

            // Arrange
            var request = new PatientInvoicesAndWalletTransactionsRequest { PatientId = 1 };
            MockDependencies();
            var handler = GetHandler();

            // Act
            var response = await handler.Handle(request, facilityId: 1);

            // Assert
            response.TotalCount.ShouldBe(3);
            response.Items.ShouldNotBeEmpty();

            response.Items[0].InvoiceAmount.Amount.ShouldBe(100);
            response.Items[0].Type.ShouldBe("PaidInvoiceItem");
            response.Items[0].InvoiceId.ShouldBe(1);

            response.Items[1].TopUpAmount.Amount.ShouldBe(2000);
            response.Items[1].Type.ShouldBe("ApproveWalletFunding");

            response.Items[2].TopUpAmount.Amount.ShouldBe(2000);
            response.Items[2].Type.ShouldBe("RequestWalletFunding");

        }


        private GetPatientInvoicesAndWalletTransactionsQueryHandler GetHandler()
        {

            var handler = new GetPatientInvoicesAndWalletTransactionsQueryHandler(_userRepository, _invoiceRepository, _invoiceItemRepository,
                _paymentActivityLogRepository, _patientAppointmentRepository);
            return handler;
        }

        private void MockDependencies()
        {

            _userRepository.GetAll().Returns(PatientInvoicesAndWalletTransactionsTestData.GetUsers().BuildMock());
            _invoiceRepository.GetAll().Returns(PatientInvoicesAndWalletTransactionsTestData.GetInvoiceAsQueryable().BuildMock());
            _invoiceItemRepository.GetAll().Returns(PatientInvoicesAndWalletTransactionsTestData.GetInvoiceItemAsQueryable().BuildMock());
            _patientAppointmentRepository.GetAll().Returns(PatientInvoicesAndWalletTransactionsTestData.GetPatientAppointmentsAsQueryable().BuildMock());
            _paymentActivityLogRepository.GetAll().Returns(PatientInvoicesAndWalletTransactionsTestData.GetPaymentActivityLogAsQueryable().BuildMock());
        }
    }
}
