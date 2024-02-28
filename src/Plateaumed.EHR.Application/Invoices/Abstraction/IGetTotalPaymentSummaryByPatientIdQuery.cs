using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Invoices.Abstraction;

public interface IGetTotalPaymentSummaryByPatientIdQuery: ITransientDependency
{
    Task<GetPatientTotalSummaryQueryResponse> Handle(long patientId,long facilityId);
}