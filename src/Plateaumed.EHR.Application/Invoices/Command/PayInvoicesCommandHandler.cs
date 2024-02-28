using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.Utility;
using Plateaumed.EHR.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;

namespace Plateaumed.EHR.Invoices.Command
{
    public class PayInvoicesCommandHandler : IPayInvoicesCommandHandler
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<Wallet, long> _walletRepository;
        private readonly IRepository<WalletHistory, long> _walletHistoryRepository;
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogRepository;
        private readonly IUnitOfWorkManager _unitOfWork;


        /// <param name="walletRepository"></param>
        /// <param name="walletHistoryRepository"></param>
        /// <param name="paymentActivityLogRepository"></param>
        /// <param name="invoiceRepository"></param>
        /// <param name="unitOfWork"></param>
        public PayInvoicesCommandHandler(
            IRepository<Wallet, long> walletRepository,
            IRepository<WalletHistory, long> walletHistoryRepository,
            IRepository<PaymentActivityLog, long> paymentActivityLogRepository,
            IRepository<Invoice, long> invoiceRepository,
            IUnitOfWorkManager unitOfWork)
        {

            _walletRepository = walletRepository;
            _walletHistoryRepository = walletHistoryRepository;
            _paymentActivityLogRepository = paymentActivityLogRepository;
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(WalletFundingRequestDto request)
        {

            using var unitOfWork = _unitOfWork.Begin();

            CheckIfItemsTotalAmountIsProperlyCalculated(request);

            var wallet = await _walletRepository.GetAll().FirstOrDefaultAsync(x => x.PatientId == request.PatientId);

            await InitializePayment(request, wallet);

            await unitOfWork.CompleteAsync();
        }


        private void CheckIfItemsTotalAmountIsProperlyCalculated(WalletFundingRequestDto request)
        {

            var calculatedTotalAmount = request.InvoiceItems.Sum(i => i.SubTotal.Amount);

            if (Math.Round(calculatedTotalAmount, 2) != request.TotalAmount.Amount)
            {
                throw new UserFriendlyException("Total amount is not equal to the sum of the items to be paid for");
            }
        }

        private async Task InitializePayment(WalletFundingRequestDto request, Wallet wallet)
        {
            ValidateWalletBalance(wallet, request);

            var groupedInvoiceItems = request.InvoiceItems
                .GroupBy(i => i.InvoiceId)
                .Select(x=> new{ invoiceId = x.FirstOrDefault()!.InvoiceId, invoiceItems = x}).ToList();

            var groupedInvoiceIds = groupedInvoiceItems.Select(x=> x.invoiceId);

            var invoices = await _invoiceRepository.GetAll().Include(i=> i.InvoiceItems)
                .Where(x=> groupedInvoiceIds.Contains(x.Id)).ToListAsync();

            foreach (var item in groupedInvoiceItems)
            {
                var invoice = invoices.FirstOrDefault(x=> x.Id == item.invoiceId);

                if (invoice != null && invoice.TotalAmount != invoice.AmountPaid)
                {
                    await InitializeInvoicePayment(invoice, wallet, item.invoiceItems);

                    await UpdateInvoicePaymentStatus(invoice, wallet);
                }

            }
        }

        private void ValidateWalletBalance(Wallet wallet, WalletFundingRequestDto request)
        {

            if (wallet == null)
            {

                throw new UserFriendlyException("Patient has no wallet");
            }

            else if (wallet.Balance < request.TotalAmount.ToMoney())
            {

                throw new UserFriendlyException("Insufficient funds, kindly top up");
            }
        }

        private async Task InitializeInvoicePayment(Invoice invoice, Wallet wallet, IGrouping<long, WalletFundingItem> groupedItems)
        {
            foreach (var requestedInvoiceItem in groupedItems)
            {
                var invoiceItem = invoice.InvoiceItems.FirstOrDefault(x => x.Id == requestedInvoiceItem.Id);

                if (invoiceItem != null && invoiceItem.Status == InvoiceItemStatus.Unpaid)
                {

                    await InitializeInvoiceItemPayment(wallet, invoice, invoiceItem);
                }

            }
        }

        // TODO(Philip): Align with Victor so we only update InvoiceItemStatus
        // since an InvoiceItem cannot be paid partially, the status is enough to know it's state.
        private async Task InitializeInvoiceItemPayment(Wallet wallet, Invoice invoice, InvoiceItem invoiceItem)
        {
            invoiceItem.AmountPaid = invoiceItem.SubTotal;
            invoiceItem.OutstandingAmount = new Money { Amount = 0, Currency = invoiceItem.SubTotal.Currency };
            invoiceItem.Status = InvoiceItemStatus.Paid;

            invoice.AmountPaid += invoiceItem.SubTotal;
            invoice.OutstandingAmount -= invoiceItem.SubTotal;

            wallet.Balance -= invoiceItem.SubTotal;

            await _walletRepository.UpdateAsync(wallet);

            await LogPaymentOfInvoiceItem(wallet, invoice, invoiceItem);
        }

        private async Task LogPaymentOfInvoiceItem(Wallet wallet, Invoice invoice, InvoiceItem invoiceItem)
        {

            // TODO(Philip): Consider to ask for Transaction Narration format.
            var transaction = new WalletHistory
            {
                WalletId = wallet.Id,
                PatientId = wallet.PatientId,
                FacilityId = invoiceItem.FacilityId,
                Amount = invoiceItem.SubTotal,
                CurrentBalance = wallet.Balance,
                TransactionType = TransactionType.Debit,
                Source = TransactionSource.Direct,
                Status = TransactionStatus.Approved,
            };

            // TODO(Philip): Consider to ask for Payment Activity Narration format.
            var paymentActivityLog = new PaymentActivityLog
            {
                TenantId = (int)invoice.TenantId,
                InvoiceItemId = invoiceItem.Id,
                FacilityId = invoice.FacilityId,
                InvoiceId = invoice.Id,
                InvoiceNo = invoice.InvoiceId,
                PatientId = wallet.PatientId,
                AmountPaid = invoiceItem.SubTotal,
                TransactionType = TransactionType.Debit,
                TransactionAction = TransactionAction.PaidInvoiceItem,
            };

            await _walletHistoryRepository.InsertAsync(transaction);
            await _paymentActivityLogRepository.InsertAsync(paymentActivityLog);
        }

        private async Task UpdateInvoicePaymentStatus(Invoice invoice, Wallet wallet) {

            if (invoice.TotalAmount == invoice.AmountPaid)
            {
                invoice.TimeOfInvoicePaid = DateTime.UtcNow;
                invoice.PaymentStatus = PaymentStatus.Paid;

                await LogFullyPaidInvoice(wallet, invoice);
            }
            else
            {
                invoice.PaymentStatus = PaymentStatus.PartiallyPaid;
            }


            await _invoiceRepository.UpdateAsync(invoice);
        }

        private async Task LogFullyPaidInvoice(Wallet wallet, Invoice invoice)
        {
            // TODO(Philip): Consider to ask for Payment Activity Narration format.
            var paymentActivityLog = new PaymentActivityLog
            {
                TenantId = (int)invoice.TenantId,
                FacilityId = invoice.FacilityId,
                InvoiceId = invoice.Id,
                InvoiceNo = invoice.InvoiceId,
                PatientId = wallet.PatientId,
                AmountPaid = invoice.AmountPaid,
                TransactionType = TransactionType.Debit,
                TransactionAction = TransactionAction.PaidInvoice,
            };

            await _paymentActivityLogRepository.InsertAsync(paymentActivityLog);
        }
    }
}
