using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Command;

public class CreateCancelInvoiceCommandHandler : ICreateCancelInvoiceCommandHandler
{
    private readonly IRepository<InvoiceCancelRequest,long> _invoiceCancelRequestRepository;
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAbpSession _abpSession;

    public CreateCancelInvoiceCommandHandler(
        IRepository<InvoiceCancelRequest, long> invoiceCancelRequestRepository,
        IRepository<InvoiceItem, long> invoiceItemRepository, 
        IUnitOfWorkManager unitOfWorkManager, 
        IAbpSession abpSession)
    {
        _invoiceCancelRequestRepository = invoiceCancelRequestRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _abpSession = abpSession;
    }
    
    /// <summary>
    /// Handler to create cancel invoice request
    /// </summary>
    /// <param name="command"></param>
    /// <param name="facilityId"></param>
    public async Task Handle(CreateCancelInvoiceCommand command, long facilityId)
    {
        var invoiceItems = await GetListOfInvoiceItemsForCancellation(command, facilityId);
        var filteredInvoiceItems = await FilterOutExistingInvoiceItemsInRequestLog(command, facilityId, invoiceItems);
        using var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew);
        foreach (var invoiceItem in filteredInvoiceItems)
        {
            invoiceItem.Status = InvoiceItemStatus.AwaitCancellationApproval;
            await _invoiceItemRepository.UpdateAsync(invoiceItem);
            await InsertInvoiceCancelRequest(command, facilityId, invoiceItem);
        }
        await uow.CompleteAsync();
    }

    #region Private Methods
    private async Task<IEnumerable<InvoiceItem>> FilterOutExistingInvoiceItemsInRequestLog(CreateCancelInvoiceCommand command, long facilityId,
        List<InvoiceItem> invoiceItems)
    {
        var existingInvoiceItemsIds = await _invoiceCancelRequestRepository
            .GetAll()
            .Where(x => x.PatientId == command.PatientId && x.FacilityId == facilityId)
            .Select(x => x.InvoiceItemId).ToListAsync();
        var filteredInvoiceItems = invoiceItems.Where(x => !existingInvoiceItemsIds.Contains(x.Id));
        return filteredInvoiceItems;
    }

    private async Task<List<InvoiceItem>> GetListOfInvoiceItemsForCancellation(CreateCancelInvoiceCommand command, long facilityId)
    {
        var query = _invoiceItemRepository.GetAll()
            .Where(x => x.FacilityId == facilityId && command.InvoiceItemsIds.Contains(x.Id));
        var invoiceItems = await query.ToListAsync();
        if (!invoiceItems.Any())
        {
            throw new UserFriendlyException("No matching invoice items to cancel");
        }

        return invoiceItems;
    }

    private async Task InsertInvoiceCancelRequest(CreateCancelInvoiceCommand command, long facilityId,
        InvoiceItem invoiceItem)
    {
        var invoiceCancelRequest = new InvoiceCancelRequest
        {
            InvoiceItemId = invoiceItem.Id,
            PatientId = command.PatientId,
            FacilityId = facilityId,
            Status = InvoiceCancelStatus.Pending,
            InvoiceId = invoiceItem.InvoiceId,
            TenantId = _abpSession.TenantId.GetValueOrDefault(),
        };
        await _invoiceCancelRequestRepository.InsertAsync(invoiceCancelRequest);
    }
    #endregion
   
}