using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Command
{
    public class ApproveDebtReliefCommandHandler : IApproveDebtReliefCommandHandler
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<PaymentActivityLog, long> _logRepository;
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
        private readonly IUnitOfWorkManager _unitOfWork;

        public ApproveDebtReliefCommandHandler(IRepository<Invoice, long> invoiceRepository,
            IRepository<PaymentActivityLog, long> logRepository,
            IRepository<InvoiceItem, long> invoiceItemRepository,
            IUnitOfWorkManager unitOfWork)
        {
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceRepository = invoiceRepository;
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ApproveDebtReliefRequestDto request)
        {
            try
            {
                var invoiceItems = await _invoiceItemRepository.GetAll().Where(x => request.SelectedInvoiceItemIds.Contains(x.Id))
                    .ToListAsync();
                var invoice = await _invoiceRepository.GetAll().Where(x => x.Id == invoiceItems[0].InvoiceId).SingleOrDefaultAsync();
                if (!invoiceItems.IsNullOrEmpty())
                {
                    var itemCount = invoiceItems.Count;
                    var discountAmount = (request.DiscountPercentage / 100) * invoice.TotalAmount.Amount;
                    var amountToDeductFromIndividualItems = discountAmount != 0 ? discountAmount / itemCount : discountAmount;
                    foreach(var item in invoiceItems)
                    {
                        if (!item.IsReliefApplied)
                        {
                            item.DebtReliefAmount = new Money(amountToDeductFromIndividualItems);
                            item.IsReliefApplied = true;
                            item.SubTotal = new Money(item.SubTotal.Amount - amountToDeductFromIndividualItems);
                            await _invoiceItemRepository.UpdateAsync(item);
                        }
                    }
          
            
                    var transactionLog = new PaymentActivityLog 
                    {
                        TenantId = request.TenantId,
                        FacilityId = request.FacilityId,
                        InvoiceId = invoiceItems[0].InvoiceId,
                        InvoiceNo = invoice.InvoiceId,
                        //ReliefAmount = new Money(discountAmount, invoiceItems[0].SubTotal.Currency),
                        //TransactionAction = TransactionAction.DebtRelief,
                        TransactionType = TransactionType.Other,
                        ActualAmount = invoice.TotalAmount,
                        Narration = "A Debt Relief Was Applied",
                        EditAmount = new Money(invoice.TotalAmount.Amount - discountAmount),
                        PatientId = invoice.PatientId
                    };
                    await _logRepository.InsertAsync(transactionLog);
                    await _unitOfWork.Current.SaveChangesAsync();
                }
            }
            catch(Exception ex) 
            {
                throw new UserFriendlyException(ex.Message);
            }
        }
    }
}
