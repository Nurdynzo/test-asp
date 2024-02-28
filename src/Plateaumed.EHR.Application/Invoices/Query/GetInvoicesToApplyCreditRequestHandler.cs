using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Query
{
    public class GetInvoicesToApplyCreditRequestHandler : IGetInvoicesToApplyCreditRequestHandler
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;

        public GetInvoicesToApplyCreditRequestHandler(IRepository<Invoice, long> invoiceRepository,
            IRepository<InvoiceItem, long> invoiceItemRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
        }

        public async Task<List<GetInvoiceToApproveCrediteDto>> Handle(long patientId)
        {
            var query = from i in _invoiceRepository.GetAll()
                        join it in _invoiceItemRepository.GetAll()
                        on i.Id equals it.InvoiceId
                        where i.PatientId == patientId && i.PaymentStatus != PaymentStatus.Paid
                        select new GetInvoiceToApproveCrediteDto
                        {
                            InvoiceId = i.Id,
                            InvoiceNumber = i.InvoiceId,
                            InvoiceDate = i.CreationTime,
                            Items = i.InvoiceItems.Select(x => new GetInvoiceItemsToApproveCreditDto
                            {
                                ItemId = x.Id,
                                ItemName = x.Name,
                                ItemAmount = new MoneyDto
                                {
                                    Amount = x.SubTotal == null ? 0 : x.SubTotal.Amount
                                },
                                InvoiceId = x.InvoiceId.GetValueOrDefault(),
                            }).ToList(),
                        };
            var result = await query.ToListAsync();
            return await query.ToListAsync(); 
        }
    }
}
