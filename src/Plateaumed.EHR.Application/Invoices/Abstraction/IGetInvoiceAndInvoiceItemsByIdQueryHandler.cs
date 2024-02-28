using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

/// <summary>
/// Interface for get invoice and invoice items
/// </summary>
public interface IGetInvoiceAndInvoiceItemsByIdQueryHandler: ITransientDependency
{
    /// <summary>
    /// Handle get invoice and invoice items by patient id
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
    Task<GetInvoiceQueryResponse> Handle(long invoiceId);
}