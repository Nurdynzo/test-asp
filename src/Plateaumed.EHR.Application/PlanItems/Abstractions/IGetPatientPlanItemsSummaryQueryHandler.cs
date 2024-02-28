using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PlanItems.Dtos;

namespace Plateaumed.EHR.PlanItems.Abstractions;

public interface IGetPatientPlanItemsSummaryQueryHandler : ITransientDependency
{
    Task<List<PlanItemsSummaryForReturnDto>> Handle(int patientId, int? tenantId, long? procedureId);
}