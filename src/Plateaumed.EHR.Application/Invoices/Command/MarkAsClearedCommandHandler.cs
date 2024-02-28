using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Command
{
    public class MarkAsClearedCommandHandler : IMarkAsClearedCommandHandler
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogs;
        private readonly IUnitOfWorkManager _unitOfWork;

        public MarkAsClearedCommandHandler(IRepository<Invoice, long> invoiceRepository,
            IRepository<PaymentActivityLog, long> paymentActivityLogs, IUnitOfWorkManager unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _paymentActivityLogs = paymentActivityLogs;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(long invoiceId)
        {
            var invoice = await GetInvoiceToMarkAsCleared(invoiceId);
            invoice.OutstandingAmount = new Money(0.00M);
            invoice.AmountPaid = invoice.TotalAmount;
            invoice.PaymentStatus = PaymentStatus.Paid;
            var logs = new List<PaymentActivityLog>();
            var log = CreateLog(invoice);
            logs.Add(log);
            logs.AddRange(CreateInvoiceItemsLogs(invoice));
            foreach(var item in logs)
            {
                await _paymentActivityLogs.InsertAsync(item);
            }
            await _unitOfWork.Current.SaveChangesAsync();
        }


        private async Task<Invoice> GetInvoiceToMarkAsCleared(long invoiceId)
        {
            var invoiceToMarkAsCleared = await _invoiceRepository.GetAll()
                .Include(i => i.InvoiceItems)
                .SingleOrDefaultAsync(x => x.Id == invoiceId);

            return invoiceToMarkAsCleared is null ? throw new UserFriendlyException("Invoice not found") : invoiceToMarkAsCleared;
        }

        private List<PaymentActivityLog> CreateInvoiceItemsLogs(Invoice invoice)
        {
            List<PaymentActivityLog> paymentActivityLogs = new();
            var invoiceItems = invoice.InvoiceItems;
            foreach (var item in invoice.InvoiceItems)
            
            {
                item.AmountPaid = item.SubTotal;
                item.Status = InvoiceItemStatus.Paid;
                var itemLog = new PaymentActivityLog
                {
                    TenantId = invoice.TenantId.GetValueOrDefault(),
                    InvoiceId = invoice.Id,
                    InvoiceNo = invoice.InvoiceId,
                    ActualAmount = item.SubTotal,
                    OutStandingAmount = new Money(0.00M),
                    PatientId = invoice.PatientId,
                    FacilityId = invoice.FacilityId,
                    InvoiceItemId = item.Id,
                    TransactionAction = TransactionAction.PaidInvoiceItem
                };
                paymentActivityLogs.Add(itemLog);
            }
            return paymentActivityLogs; 
        }

        private PaymentActivityLog CreateLog(Invoice invoice)
        {
            return new PaymentActivityLog
            {
                TenantId = invoice.TenantId.GetValueOrDefault(),
                InvoiceId = invoice.Id,
                InvoiceNo = invoice.InvoiceId,
                AmountPaid = invoice.TotalAmount,
                OutStandingAmount = new Money(0.00M),
                PatientId = invoice.PatientId,
                FacilityId = invoice.FacilityId,
                TransactionAction = TransactionAction.ClearInvoice,
                Narration = "Invoice was cleared"
            };
        }
    }
}