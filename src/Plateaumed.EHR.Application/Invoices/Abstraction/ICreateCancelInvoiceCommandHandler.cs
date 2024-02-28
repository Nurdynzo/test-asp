using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

public interface ICreateCancelInvoiceCommandHandler: ITransientDependency
{
    Task Handle(CreateCancelInvoiceCommand command, long facilityId);
}