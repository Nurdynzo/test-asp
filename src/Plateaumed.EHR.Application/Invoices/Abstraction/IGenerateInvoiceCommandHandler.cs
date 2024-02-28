using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

/// <summary>
/// This is a command handler for the GenerateInvoiceCommand.
/// </summary>
public interface IGenerateInvoiceCommandHandler: ITransientDependency
{
    /// <summary>
    /// this method handles the GenerateInvoiceCommand.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<string> Handle(GenerateInvoiceCommand command);
}