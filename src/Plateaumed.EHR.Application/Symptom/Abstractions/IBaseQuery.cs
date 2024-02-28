using System.Linq;
using Abp.Dependency;

namespace Plateaumed.EHR.Symptom.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    IQueryable<AllInputs.Symptom> GetPatientSymptomsBaseQuery(long patientId, int? tenantId, long? encounterId = null);
}
