using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Invoices.Command;
/// <summary>
/// Update invoice request handler
/// </summary>
public class UpdateInvoiceCommandHandler : IUpdateInvoiceCommandHandler
{
    private readonly IRepository<Invoice, long> _invoiceRepository;
    private readonly IRepository<InvoiceItem, long> _invoiceItemRepository;
    private readonly IUnitOfWorkManager _unitOfWork;
    private readonly IObjectMapper _objectMapper;

    /// <summary>
    /// Constructor for the <see cref="UpdateInvoiceCommandHandler"/>
    /// </summary>
    /// <param name="invoiceRepository"></param>
    /// <param name="invoiceItemRepository"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="objectMapper"></param>
    public UpdateInvoiceCommandHandler(
        IRepository<Invoice, long> invoiceRepository,
        IRepository<InvoiceItem, long> invoiceItemRepository,
        IUnitOfWorkManager unitOfWork,
        IObjectMapper objectMapper )
    {
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _unitOfWork = unitOfWork;
        _objectMapper = objectMapper;
    }

    /// <summary>
    /// Handle the update invoice request
    /// </summary>
    /// <param name="request"></param>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task Handle(UpdateNewInvoiceRequest request)
    {
          var invoice = CheckIfInvoiceExist(request.Id);
          
          await RemoveInvoiceItems(request, invoice);

          await AddInvoiceItemsIfNew(request, invoice);
          
          CheckIfAmountCalculatedCorrectly(request, invoice);

          invoice.IsEdited = true;

          await _unitOfWork.Current.SaveChangesAsync();

    }

    private Invoice CheckIfInvoiceExist(long invoiceId) {

        var invoice = _invoiceRepository.GetAllIncluding(x => x.InvoiceItems)
                .FirstOrDefault(x => x.Id == invoiceId);

        if (invoice == null) {

            throw new UserFriendlyException("Invoice not found");
        }

        return invoice;
    }

    /// <summary>
    /// Check if amount is calculated correctly
    /// </summary>
    /// <param name="request"></param>
    /// <param name="invoice"></param>
    /// <exception cref="UserFriendlyException"></exception>
    private static void CheckIfAmountCalculatedCorrectly(UpdateNewInvoiceRequest request, Invoice invoice)
    {
        decimal currentAmount = request.Items.Where(x => !x.IsDeleted).Sum(x =>
            x.DiscountPercentage > 0
                ? (x.UnitPrice.Amount * x.Quantity) - (x.UnitPrice.Amount * x.Quantity) * x.DiscountPercentage / 100
                : x.UnitPrice.Amount * x.Quantity);
        if (request.TotalAmount.Amount != Math.Round(currentAmount, 2))
        {
            throw new UserFriendlyException("Total amount is not equal to the sum of the items to be saved");
        }

        invoice.TotalAmount = new Money(request.TotalAmount.Amount, request.TotalAmount.Currency);
    }
    
    /// <summary>
    /// Add invoice items if new 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="invoice"></param>

    private async Task AddInvoiceItemsIfNew(UpdateNewInvoiceRequest request, Invoice invoice)
    {
        foreach (var items in request.Items.Where(x => !x.IsDeleted))
        {
            var item = invoice.InvoiceItems.FirstOrDefault(x => x.Id == items.Id);
            if (item == null)
            {
                await _invoiceItemRepository.InsertAsync(_objectMapper.Map<InvoiceItem>(items));
            }
        }
    }
    
    /// <summary>
    /// Remove invoice items if is marked as deleted
    /// </summary>
    /// <param name="request"></param>
    /// <param name="invoice"></param>
    /// <exception cref="UserFriendlyException"></exception>

    private async Task RemoveInvoiceItems(UpdateNewInvoiceRequest request, Invoice invoice)
    {
        foreach (var items in request.Items.Where(x => x.IsDeleted))
        {
            var item = invoice.InvoiceItems.FirstOrDefault(x => x.Id == items.Id);
            if (item != null)
            {
                if (item.AmountPaid > 0)
                    throw new UserFriendlyException("Cannot delete item that has been paid for");
                await _invoiceItemRepository.DeleteAsync(item);
            }
        }
    }
}