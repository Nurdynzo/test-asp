using System.Collections.Generic;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Dtos;
using System.Linq;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.PatientWallet.Query
{
    /// <inheritdoc/>
    public class GetWalletFundingRequestsQueryHandler : IGetWalletFundingRequestsQueryHandler
    {
        private readonly IRepository<WalletHistory, long> _walletHistoryRepository;
        private readonly IRepository<Invoice,long> _invoiceRepository;
        private readonly IRepository<InvoiceItem,long> _invoiceItemRepository;





        /// <param name="walletHistoryRepository"></param>
        /// <param name="invoiceRepository"></param>
        /// <param name="invoiceItemRepository"></param>
        public GetWalletFundingRequestsQueryHandler(
            IRepository<WalletHistory, long> walletHistoryRepository, 
            IRepository<Invoice, long> invoiceRepository, 
            IRepository<InvoiceItem, long> invoiceItemRepository)

        {

            _walletHistoryRepository = walletHistoryRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
        }

        /// <inheritdoc/>
        public async Task<WalletFundingResponseDto> Handle(WalletFundingRequestsDto request)
        {
            var walletFundingQuery = await _walletHistoryRepository.GetAll()
                                    .AsNoTracking()
                                    .Where(x => x.PatientId == request.PatientId 
                                    && x.Status == TransactionStatus.Pending
                                    && x.Source == TransactionSource.Indirect
                                    && x.TransactionType == TransactionType.Credit).ToListAsync();

            var amountToBeFunded = walletFundingQuery.Sum(x => x.Amount);

            if (amountToBeFunded <= 0.00m.ToMoney(amountToBeFunded.Currency)) {
                return new WalletFundingResponseDto();
            }

            var invoicesQuery = await GetInvoiceQuery(request);

            var totalAmount = invoicesQuery.Sum(x=> x.TotalAmount.ToMoney());
            
            return new WalletFundingResponseDto {
                Invoices = invoicesQuery,
                TotalAmount = totalAmount.ToMoneyDto(),
                AmountToBeFunded = amountToBeFunded.ToMoneyDto(),
            };
        }
        private async Task<List<UnpaidInvoiceDto>> GetInvoiceQuery(WalletFundingRequestsDto request)
        {

            var invoicesQuery = await (from i in _invoiceRepository.GetAll().AsNoTracking()
                                       join it in _invoiceItemRepository.GetAll() on i.Id equals it.InvoiceId
                                       where i.PatientId == request.PatientId && it.Status == InvoiceItemStatus.AwaitingApproval
                                       group it by new
                                       {
                                           i.Id,
                                           i.InvoiceId,
                                          i.CreationTime
                                       } into g
                                       select new UnpaidInvoiceDto
                                       {
                                           Id = g.Key.Id,
                                           InvoiceNo = g.Key.InvoiceId,
                                           IssuedOn = g.Key.CreationTime,
                                           TotalAmount = new MoneyDto
                                           {
                                               Amount = g.Sum(x => x.SubTotal.Amount),
                                               Currency = g.FirstOrDefault().SubTotal.Currency,
                                           },
                                           InvoiceItems = g.Select(x =>  new UnpaidInvoiceItemDto
                                           {
                                               Id = x.Id,
                                               Name = x.Name,
                                               DiscountPercentage = x.DiscountPercentage ?? 0,
                                               IsGlobal = x.DiscountPercentage.HasValue & x.DiscountPercentage.Value > 0,
                                               SubTotal = x.SubTotal.ToMoneyDto(),
                                           }).ToList(),

                                       }).ToListAsync();
            return invoicesQuery;
        }
    }
}
