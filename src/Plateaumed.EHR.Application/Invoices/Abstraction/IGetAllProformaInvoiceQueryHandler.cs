using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

public interface IGetAllProformaInvoiceQueryHandler: ITransientDependency
{
    Task<GetAllProformaInvoiceQueryResponse> Handle(long patientId, long facilityId);
}