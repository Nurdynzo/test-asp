using System.Linq;
using Abp.Dependency;

namespace Plateaumed.EHR.PlanItems.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    IQueryable<AllInputs.PlanItems> GetPatientPlanItemsBaseQuery(int patientId, long? procedureId = null, bool? isDeleted = null);
}
