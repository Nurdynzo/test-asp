using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;
using Plateaumed.EHR.PatientWallet.Query;
using Plateaumed.EHR.Tests.Invoices.Util;
using Plateaumed.EHR.Tests.PatientWallet.Utils;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientWallet.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetWalletFundingRequestsQueryHandlerTests
    {
        private readonly IRepository<WalletHistory, long> _walletHistoryRepositoryMock = Substitute.For<IRepository<WalletHistory, long>>();
        private readonly IRepository<Invoice, long> _invoiceRepositoryMock = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepositoryMock = Substitute.For<IRepository<InvoiceItem, long>>();

        [Fact]
        public async Task Handle_GivenPatientHasNoWalletFundingRequests_ShouldReturnNullValues()
        {

            // Arrange
            MockDependencies();
            var request = new WalletFundingRequestsDto { PatientId = 10 };
            var handler = GetHandler();

            // Act
            var response = await handler.Handle(request);

            // Assert
            response.AmountToBeFunded.ShouldBe(null);
            response.TotalAmount.ShouldBe(null);
            response.Invoices.ShouldBe(null);
        }

        [Fact]
        public async Task Handle_GivenPatientHasWalletFundingRequests_ShouldReturnRequests() 
        {

            // Arrange
            MockDependencies();
            var request = new WalletFundingRequestsDto { PatientId = 1 };
            var handler = GetHandler();

            // Act
            var response = await handler.Handle(request);

            // Assert
            response.AmountToBeFunded.ShouldBe(2000.00m.ToMoneyDto());
            response.TotalAmount.ShouldBe(681.58m.ToMoneyDto());
            response.Invoices.Count.ShouldBe(1);
            response.Invoices[0].InvoiceItems.Count().ShouldBe(2);
           
        }


        private void MockDependencies()
        {
            _walletHistoryRepositoryMock.GetAll().Returns(CommonUtils.GetWalletHistories().BuildMock());
            _invoiceRepositoryMock.GetAll().Returns(CommonUtils.GetInvoiceWithItemsAwaitingApprovalAsQueryable().BuildMock());
            _invoiceItemRepositoryMock.GetAll().Returns(CommonUtils.GetInvoiceItemsAwaitingApprovalAsQueryable().BuildMock());
        }

        private GetWalletFundingRequestsQueryHandler GetHandler()
        {
            var handler = new GetWalletFundingRequestsQueryHandler(
                _walletHistoryRepositoryMock, 
                _invoiceRepositoryMock,
                _invoiceItemRepositoryMock);
            return handler;
        }

 

    }
}
