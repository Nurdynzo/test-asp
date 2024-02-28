using System.Threading.Tasks;
using Abp.Dependency;
using Abp.UI;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

/// <summary>
/// Update invoice request handler
/// </summary>
public interface IUpdateInvoiceCommandHandler: ITransientDependency
{
    /// <summary>
    /// Handle the update invoice request
    /// </summary>
    /// <param name="request"></param>
    /// <exception cref="UserFriendlyException"></exception>
    Task Handle(UpdateNewInvoiceRequest request);
}