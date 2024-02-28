using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.Utility;
using Plateaumed.EHR.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;

namespace Plateaumed.EHR.PatientWallet.Command
{
    public class CreateWalletFundingRequestHandler : ICreateWalletFundingRequestHandler
    {
        private readonly IRepository<Wallet, long> _walletRepository;
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
        private readonly IRepository<WalletHistory, long> _walletHistoryRepository;
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogRepository;
        private readonly IUnitOfWorkManager _unitOfWork;


        /// <param name="walletRepository"></param>
        /// <param name="invoiceItemRepository"></param>
        /// <param name="walletHistoryRepository"></param>
        /// <param name="paymentActivityLogRepository"></param>
        /// <param name="unitOfWork"></param>
        public CreateWalletFundingRequestHandler(
            IRepository<Wallet, long> walletRepository,
            IRepository<InvoiceItem, long> invoiceItemRepository,
            IRepository<WalletHistory, long> walletHistoryRepository,
            IRepository<PaymentActivityLog, long> paymentActivityLogRepository,
            IUnitOfWorkManager unitOfWork)
        {

            _walletRepository = walletRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _walletHistoryRepository = walletHistoryRepository;
            _paymentActivityLogRepository = paymentActivityLogRepository;
            _unitOfWork = unitOfWork;

        }

        /// <inheritdoc/>
        public async Task Handle(WalletFundingRequestDto request, long facilityId, int tenantId)
        {

            var wallet = await GetPatientWallet(request.PatientId);

            var amountToBeFunded = request.AmountToBeFunded.ToMoney();
            ValidateInvoiceItemsCanBePaidFor(request.InvoiceItems, wallet, amountToBeFunded);

            // TODO(Philip): Consider to ask for Transaction Narration format.
            var walletHistory = new WalletHistory
            {
                WalletId = wallet.Id,
                PatientId = request.PatientId,
                FacilityId = facilityId,
                Amount = amountToBeFunded,
                CurrentBalance = wallet.Balance,
                TransactionType = TransactionType.Credit,
                Source = TransactionSource.Indirect,
                Status = TransactionStatus.Pending,
                TenantId = tenantId
            };

            // TODO(Philip): Consider to ask for Payment Activity Narration format.
            var paymentActivityLog = new PaymentActivityLog
            {
                TenantId = tenantId,
                ToUpAmount = amountToBeFunded,
                PatientId = request.PatientId,
                FacilityId = facilityId,
                TransactionType = TransactionType.Credit,
                TransactionAction = TransactionAction.RequestWalletFunding,
            };

            await LockWalletFundingRequestItems(request);
            await _walletHistoryRepository.InsertAsync(walletHistory);
            await _paymentActivityLogRepository.InsertAsync(paymentActivityLog);
            await _unitOfWork.Current.SaveChangesAsync();
        }


        private async Task<Wallet> GetPatientWallet(long patientId)
        {

            var wallet = await _walletRepository.FirstOrDefaultAsync(x=>x.PatientId == patientId);

            if (wallet == null)
            {
                var newWallet = new Wallet
                {
                    PatientId = patientId,
                    Balance = new Money { Amount = 0 }
                };

                await _walletRepository.InsertAsync(newWallet);
                await _unitOfWork.Current.SaveChangesAsync();
                return newWallet;
            }

            return wallet;
        }

        private void ValidateInvoiceItemsCanBePaidFor(ICollection<WalletFundingItem> invoiceItems, Wallet wallet, Money amountToBeFunded)
        {
            var totalAmountOfItems = invoiceItems.Sum(x=>x.SubTotal.ToMoney());

            var expectedBalance = wallet.Balance + amountToBeFunded;

            if (totalAmountOfItems > expectedBalance)
            {
                throw new UserFriendlyException($"Total amount of invoice items {totalAmountOfItems.Amount} should not be more than the sum of wallet balance {wallet.Balance.Amount} and amount to be funded ${amountToBeFunded.Amount}");
            }
        }

        private async Task LockWalletFundingRequestItems(WalletFundingRequestDto request)
        {
            var invoiceItemIds = request.InvoiceItems.Select(x=> x.Id);
            var invoiceItems = await _invoiceItemRepository.GetAll()
                .Where(i => i.Status == InvoiceItemStatus.Unpaid
                && invoiceItemIds.Contains(i.Id)).ToListAsync();
            
            foreach (var invoiceItem in invoiceItems) { 
                invoiceItem.Status = InvoiceItemStatus.AwaitingApproval;
                await _invoiceItemRepository.UpdateAsync(invoiceItem);
            }
        }
    }
}
