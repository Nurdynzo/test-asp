using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

public interface IProcessInvoiceCancelRequestCommandHandler: ITransientDependency
{
    Task Handle(ApproveCancelInvoiceCommand request, long facilityId);
}