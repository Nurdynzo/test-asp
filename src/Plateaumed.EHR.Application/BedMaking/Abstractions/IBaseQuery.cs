using System.Linq;
using Abp.Dependency;

namespace Plateaumed.EHR.BedMaking.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    IQueryable<AllInputs.BedMaking> GetPatientBedMakingBaseQuery(int patientId, int? tenantId, bool isDeleted = false);
}