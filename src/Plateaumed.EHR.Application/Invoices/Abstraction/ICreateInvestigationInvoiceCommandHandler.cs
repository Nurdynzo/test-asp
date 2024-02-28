using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface ICreateInvestigationInvoiceCommandHandler: ITransientDependency
    {
        Task<CreateNewInvestigationInvoiceCommand> Handle(CreateNewInvestigationInvoiceCommand command, long facilityId);
    }
}

