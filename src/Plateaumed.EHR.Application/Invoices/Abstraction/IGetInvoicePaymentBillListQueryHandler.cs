using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

/// <summary>
/// Get Payment Bills for a Patient Handler
/// </summary>
public interface IGetInvoicePaymentBillListQueryHandler: ITransientDependency
{
    /// <summary>
    /// Handle Get Payment Bills for a Patient
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<InvoiceReceiptQueryResponse> Handle(GetInvoicePaymentBillListQueryRequest request);
}