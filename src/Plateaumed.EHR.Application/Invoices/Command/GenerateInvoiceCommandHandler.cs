using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Invoices.Command;

/// <inheritdoc />
public class GenerateInvoiceCommandHandler : IGenerateInvoiceCommandHandler
{
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private const int InvoiceSerialNumberLength = 10;

    /// <summary>
    /// Constructor for the GenerateInvoiceCommandHandler.
    /// </summary>
    /// <param name="invoiceRepository"></param>
    public GenerateInvoiceCommandHandler(IRepository<Invoice, long> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    /// <inheritdoc />
    public async Task<string> Handle(GenerateInvoiceCommand command)
    {
        var lastInvoice =  await _invoiceRepository.GetAll()
            .Select(x=> new {x.Id,x.InvoiceId,x.TenantId})
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(x=>x.TenantId == command.TenantId);;
        var lastInvoiceCode = lastInvoice?.InvoiceId;
        var invoiceCode = "1".PadLeft(InvoiceSerialNumberLength, '0');
        if (string.IsNullOrWhiteSpace(lastInvoiceCode)) return invoiceCode;
        var lastInvoiceSerialNumberInt = long.Parse(lastInvoiceCode);
       invoiceCode = (lastInvoiceSerialNumberInt + 1).ToString().PadLeft(InvoiceSerialNumberLength, '0');
       return invoiceCode;

    }
}