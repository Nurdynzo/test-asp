using System;
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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientWallet.UnitTest
{
    [Trait("Category", "Unit")]
    public class CreateWalletFundingRequestCommandHandlerTests 
    {
        private readonly IRepository<Wallet, long> _walletRepository = Substitute.For<IRepository<Wallet, long>>();
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository = Substitute.For<IRepository<InvoiceItem, long>>();
        private readonly IRepository<WalletHistory, long> _walletHistoryRepository = Substitute.For<IRepository<WalletHistory, long>>();
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogRepository = Substitute.For<IRepository<PaymentActivityLog, long>>();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

        [Fact]
        public async Task Handle_GivenSumOfWalletBalanceAndTopUpAmountIsInsufficient_ShouldThrowException() {

            //Arrange
            var tenantId = 1;
            var facilityId = 1; 
            var patientId = 1;
            var topUpAmount = 10.00M;
            var request = CommonUtils.GetWalletFundingRequestsDto(amountToBeFunded: topUpAmount);
            var totalAmountOfItems = request.InvoiceItems.Sum(i=> i.SubTotal.Amount); 
            var wallet = CommonUtils.GetNewWallet(patientId: patientId);
            MockDependencies(wallet, patientId);
            var handler = GetHandler();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, tenantId, facilityId));
            message.Message.ShouldBe($"Total amount of invoice items {totalAmountOfItems} should not be more than the sum of wallet balance {wallet.Balance.Amount} and amount to be funded ${topUpAmount}");
        }


        [Fact]
        public async Task Handle_GivenSumOfWalletBalanceAndTopUpAmountIsSufficient_ShouldCreateFundingRequest()
        {

            //Arrange
            var tenantId = 1;
            var facilityId = 1;
            var patientId = 1;
            var request = CommonUtils.GetWalletFundingRequestsDto(1000.00M);
            var wallet = CommonUtils.GetNewWallet(patientId: patientId);
            MockDependencies(wallet, patientId);
            var handler = GetHandler();

            // Act & Assert
            await Should.NotThrowAsync(async () => await handler.Handle(request, tenantId, facilityId));
            await _unitOfWork.Current.Received(1).SaveChangesAsync();
        }


        private CreateWalletFundingRequestHandler GetHandler() {
            var handler = new CreateWalletFundingRequestHandler(_walletRepository, 
                _invoiceItemRepository, _walletHistoryRepository, _paymentActivityLogRepository,
                _unitOfWork);

            return handler;
        }

        private void MockDependencies(Wallet wallet, int patientId = 1) {
            
            _walletRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Wallet, bool>>>()).Returns(wallet);

            var unpaidInvoiceItems = CommonUtils.GetUnpaidInvoiceItems().BuildMock();
            _invoiceItemRepository.GetAll().Returns(unpaidInvoiceItems);
            _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        }

    }
}
