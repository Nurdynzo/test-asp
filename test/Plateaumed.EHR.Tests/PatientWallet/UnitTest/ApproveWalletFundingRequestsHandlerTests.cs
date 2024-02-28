

using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.PatientWallet.Command;
using Plateaumed.EHR.Tests.PatientWallet.Utils;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientWallet.UnitTest
{
    [Trait("Category", "Unit")]
    public class ApproveWalletFundingRequestsHandlerTests
    {
        private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<Wallet, long> _walletRepository = Substitute.For<IRepository<Wallet, long>>();
        private readonly IRepository<WalletHistory, long> _walletHistoryRepository = Substitute.For<IRepository<WalletHistory, long>>();
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogRepository = Substitute.For<IRepository<PaymentActivityLog, long>>();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

        [Fact]
        public async Task Handle_GivenItemsTotalAmountIsNotProperlyCalculated_ShouldThrowException()
        {

            // Arrange
            var wallet = CommonUtils.GetNewWallet();
            var request = CommonUtils.GetWalletFundingRequestsForInvoiceItemsAwaitingApproval(totalAmount: 2000);
            MockDependencies(wallet);
            var handler = GetHandler();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, tenantId: 1));
            message.Message.ShouldBe("Total amount is not equal to the sum of the items to be paid for");
        }

        [Fact]
        public async Task Handle_GivenAmountToBeFundedDoesEqualSumOfPendingWalletFunds_ShouldThrowException()
        {

            // Arrange
            var wallet = CommonUtils.GetNewWallet(balance: 2500);
            var request = CommonUtils.GetWalletFundingRequestsForInvoiceItemsAwaitingApproval(amountToBeFunded: 1000);
            MockDependencies(wallet);
            var handler = GetHandler();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, tenantId: 1));
            message.Message.ShouldBe("Amount to be funded is not valid");
        }
        
        [Fact]
        public async Task Handle_GivenInsufficientWalletBalance_ShouldThrowException()
        {

            // Arrange
            var wallet = CommonUtils.GetNewWallet(balance: -3000);
            var request = CommonUtils.GetWalletFundingRequestsForInvoiceItemsAwaitingApproval();
            MockDependencies(wallet);
            var handler = GetHandler();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, tenantId: 1));
            message.Message.ShouldBe("Insufficient funds, kindly top up");
        }


        [Fact]
        public async Task Handle_GivenSufficientWalletBalance_ShouldCompletePayment()
        {

            // Arrange
            var wallet = CommonUtils.GetNewWallet(balance: 2500);
            var request = CommonUtils.GetWalletFundingRequestsForInvoiceItemsAwaitingApproval();
            MockDependencies(wallet);
            var handler = GetHandler();

            // Act & Assert
            await Should.NotThrowAsync(async () => await handler.Handle(request, tenantId: 1));
            await _paymentActivityLogRepository.Received(5).InsertAsync(Arg.Any<PaymentActivityLog>());
            await _walletHistoryRepository.Received(2).UpdateAsync(Arg.Any<WalletHistory>());
        }

        private ApproveWalletFundingRequestsHandler GetHandler()
        {
            var handler = new ApproveWalletFundingRequestsHandler(_invoiceRepository,
                _walletRepository, _walletHistoryRepository,
                _paymentActivityLogRepository, _unitOfWork);
            return handler;
        }

        private void MockDependencies(Wallet wallet)
        {
            var invoices = CommonUtils.GetInvoicesWithItemsAwaitingApprovalAsQueryable().BuildMock();
            var walletHistories = CommonUtils.GetWalletHistories(walletBalance: wallet.Balance.Amount).BuildMock();

            _invoiceRepository.GetAll().Returns(invoices);

            var wallets = new List<Wallet>() { wallet }.AsQueryable().BuildMock();
            _walletRepository.GetAll().Returns(wallets);
            _walletHistoryRepository.GetAll().Returns(walletHistories);
            _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        }

    }
}
