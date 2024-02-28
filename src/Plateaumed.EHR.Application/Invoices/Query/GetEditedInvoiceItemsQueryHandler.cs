using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Query
{
    public class GetEditedInvoiceItemsQueryHandler : IGetEditedInvoiceItemsQueryHandler
    {
        private readonly IRepository<PaymentActivityLog, long> _transactionLog;
        private readonly IRepository<InvoiceItem, long> _itemRepository;
        private readonly IRepository<Invoice, long> _invoiceRepository;

        public GetEditedInvoiceItemsQueryHandler(IRepository<InvoiceItem, long> itemRepository,
            IRepository<PaymentActivityLog, long> transactionLog,
            IRepository<Invoice, long> invoiceRepository)
        {
            _transactionLog = transactionLog;
            _itemRepository = itemRepository;
            _invoiceRepository = invoiceRepository;
        }


        public async Task<List<GetEditedInvoiceItemResponseDto>> Handle(long invoiceId)
        {
            var query = (from l in _transactionLog.GetAllIncluding(x => x.Invoice)
                            join i in _itemRepository.GetAll() on l.InvoiceId equals i.InvoiceId
                            where l.InvoiceId == invoiceId
                            select new GetEditedInvoiceItemResponseDto
                            {
                                InvoiceNumber = l.InvoiceNo,
                                ItemName = i.Name,
                                PaymentType = l.Invoice.PaymentType.ToString(),
                                AmountBeforeEdit = new MoneyDto
                                {
                                    Amount = l.ActualAmount == null ? 0 : l.ActualAmount.Amount,
                                    Currency = l.ActualAmount == null ? "" :l.ActualAmount.Currency
                                },
                                EditedInvoice = new MoneyDto
                                {
                                    Amount = l.EditAmount == null ? 0 : l.EditAmount.Amount,
                                    Currency = l.EditAmount == null ? "" : l.EditAmount.Currency,
                                },
                                AmountPaid = new MoneyDto
                                {
                                    Amount = l.AmountPaid == null ? 0 : l.AmountPaid.Amount,
                                    Currency = l.AmountPaid == null ? "" : l.AmountPaid.Currency,
                                },
                                Outstanding = new MoneyDto
                                {
                                    Amount = l.OutStandingAmount == null ? 0 : l.OutStandingAmount.Amount,
                                    Currency = l.OutStandingAmount == null ? "" : l.OutStandingAmount.Currency
                                }
                            }).AsQueryable();

            return await query.ToListAsync();
        }
    }
}
