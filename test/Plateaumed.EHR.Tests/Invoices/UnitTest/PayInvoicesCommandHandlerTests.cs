using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Command;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.Tests.Invoices.Util;
using System.Threading.Tasks;
using Xunit;
using MockQueryable.NSubstitute;
using Abp.UI;
using Shouldly;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    [Trait("Category", "Unit")]
    public class PayInvoicesCommandHandlerTests
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
            var wallet = CommonUtil.GetNewWallet();
            var request = CommonUtil.GetWalletFundingRequestsDto(totalAmount: 2000);
            MockDependencies(wallet);
            var handler = GetHandler();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async ()=> await handler.Handle(request));
            message.Message.ShouldBe("Total amount is not equal to the sum of the items to be paid for");
        }
        
        [Fact]
        public async Task Handle_GivenInsufficientWalletBalance_ShouldThrowException() 
        {

            // Arrange
            var wallet = CommonUtil.GetNewWallet();
            var request = CommonUtil.GetWalletFundingRequestsDto();
            MockDependencies(wallet);
            var handler = GetHandler();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async ()=> await handler.Handle(request));
            message.Message.ShouldBe("Insufficient funds, kindly top up");
        }
        
        
        [Fact]
        public async Task Handle_GivenSufficientWalletBalance_ShouldCompletePayment()  
        {

            // Arrange
            var wallet = CommonUtil.GetNewWallet(balance: 2500);
            var request = CommonUtil.GetWalletFundingRequestsDto();
            MockDependencies(wallet);
            var handler = GetHandler();

            // Act & Assert
            await Should.NotThrowAsync(async ()=> await handler.Handle(request));
        }

        private PayInvoicesCommandHandler GetHandler()
        {
            var handler = new PayInvoicesCommandHandler(_walletRepository, _walletHistoryRepository,
                _paymentActivityLogRepository, _invoiceRepository, _unitOfWork);
            return handler;
        }

        private void MockDependencies(Wallet wallet)
        {
            var invoices = CommonUtil.GetInvoicesToBePaidForAsQueryable().BuildMock();

            _invoiceRepository.GetAll()
                .Returns(invoices);

            var wallets = new List<Wallet>() { wallet }.AsQueryable().BuildMock();
            _walletRepository.GetAll().Returns(wallets);
            _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        }
    }
}
