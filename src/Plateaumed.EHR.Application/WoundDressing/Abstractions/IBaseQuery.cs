using System.Linq;
using Abp.Dependency;

namespace Plateaumed.EHR.WoundDressing.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    IQueryable<AllInputs.WoundDressing> GetPatientWoundDressingBaseQuery(int patientId, bool isDeleted = false);
}