using System.Collections.Generic;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Misc;
using System.Linq;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Query.BaseQueryHelper
{
    public class InvoicesBaseQuery : IInvoicesBaseQuery
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;

        public InvoicesBaseQuery(IRepository<Invoice, long> invoiceRepository)

        {
            _invoiceRepository = invoiceRepository;
        }

        public IQueryable<UnpaidInvoiceDto> GetPatientUnpaidInvoicesBaseQuery(long patientId, InvoiceItemStatus invoiceItemStatus)
        {
            var query = _invoiceRepository.GetAll()
                .Include(x => x.InvoiceItems)
                .AsNoTracking()
                .Where(x => x.PatientId == patientId && (x.PaymentStatus == PaymentStatus.Unpaid || x.PaymentStatus == PaymentStatus.PartiallyPaid))
                .Select(x => new UnpaidInvoiceDto
                {
                    Id = x.Id,
                    InvoiceNo = x.InvoiceId,
                    IssuedOn = x.CreationTime.Date,
                    TotalAmount = x.InvoiceItems != null ? x.InvoiceItems
                        .Where(i => i.Status == invoiceItemStatus)
                        .Select(i => i.SubTotal).Sum().ToMoneyDto() : new MoneyDto(),
                    InvoiceItems = x.InvoiceItems != null ? x.InvoiceItems
                        .Where(i => i.Status == invoiceItemStatus)
                        .Select(i => new UnpaidInvoiceItemDto
                        {
                            Id = i.Id,
                            Name = i.Name,
                            DiscountPercentage = i.DiscountPercentage ?? 0,
                            IsGlobal = i.DiscountPercentage != null && i.DiscountPercentage > 0,
                            SubTotal = i.SubTotal != null ? i.SubTotal.ToMoneyDto() : new MoneyDto(),
                        }).ToList() : new List<UnpaidInvoiceItemDto>()
                });

            return query;
        }
    }
}
