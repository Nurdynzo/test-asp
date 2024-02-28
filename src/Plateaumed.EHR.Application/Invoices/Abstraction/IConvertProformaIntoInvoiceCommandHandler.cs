using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

public interface IConvertProformaIntoInvoiceCommandHandler : ITransientDependency
{
    Task Handle(ProformaToNewInvoiceRequest request, long facilityId);
}