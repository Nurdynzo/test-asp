using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

/// <summary>
/// handler for creating a new invoice
/// </summary>
public interface ICreateNewInvoiceCommandHandler : ITransientDependency
{
    /// <summary>
    /// handles the creation of a new invoice
    /// </summary>
    /// <param name="command"></param>
    /// <param name="facilityId"></param>
    /// <returns></returns>
    Task<CreateNewInvoiceCommand> Handle(CreateNewInvoiceCommand command, long facilityId);
}
