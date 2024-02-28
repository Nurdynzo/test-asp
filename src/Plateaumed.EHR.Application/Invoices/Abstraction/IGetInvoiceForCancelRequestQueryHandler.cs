using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

public interface IGetInvoiceForCancelRequestQueryHandler: ITransientDependency
{
    Task<List<GetInvoiceForCancelQueryResponse>> Handle(long patientId, long facilityId);
}