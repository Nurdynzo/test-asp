using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using Plateaumed.EHR.Misc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Invoices.Query
{
    public class GetItemsToApplyReliefQueryHandler : IGetItemsToApplyReliefQueryHandler
    {
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<User,long> _userRepository;
        private readonly IAbpSession _abpSession;
        public GetItemsToApplyReliefQueryHandler(IRepository<InvoiceItem, long> invoiceItemRepository,
            IRepository<Invoice, long> invoiceRepository, 
            IRepository<User, long> userRepository, 
            IAbpSession abpSession)
        {
            _invoiceItemRepository = invoiceItemRepository;   
            _invoiceRepository = invoiceRepository;
            _userRepository = userRepository;
            _abpSession = abpSession;
        }
        public async Task<List<ApplyReliefInvoiceViewDto>> Handle(long patientId)
        {
            var query = from i in _invoiceRepository.GetAll()
                        join items in _invoiceItemRepository.GetAll() on i.Id equals items.InvoiceId
                        from user in _userRepository.GetAll().Where(x=>x.Id == _abpSession.UserId)
                        where i.PaymentStatus == PaymentStatus.Unpaid && i.PatientId == patientId && !items.IsReliefApplied
                        orderby i.CreationTime descending
                        group items by new
                        {
                            InvoiceNumber = i.InvoiceId,
                            ParentInvoiceId = i.Id,
                            InvoiceDate = i.CreationTime,
                            user
                        }
                    into itemGroups
                        select new ApplyReliefInvoiceViewDto
                        {
                            Id = itemGroups.Key.ParentInvoiceId,
                            InvoiceNumber = itemGroups.Key.InvoiceNumber,
                            InvoiceDate = itemGroups.Key.InvoiceDate,
                            InitiatedBy = $"{itemGroups.Key.user.Title}. {itemGroups.Key.user.Name}",
                            GroupedInvoiceItems = itemGroups.Select(x => new ApplyReliefItemsViewDto
                            {
                                Id = x.Id,
                                ItemName = x.Name,
                                Price = new MoneyDto { Amount = x.SubTotal == null ? 0 : x.SubTotal.Amount },
                                ReliefAmount = new MoneyDto 
                                { 
                                    Amount = x.DebtReliefAmount == null ? 0 : x.DebtReliefAmount.Amount 
                                },
                                IsReliefApplied = x.IsReliefApplied
                            }).ToList()
                        };

            return await query.ToListAsync();
            
            
        }
    }
}
