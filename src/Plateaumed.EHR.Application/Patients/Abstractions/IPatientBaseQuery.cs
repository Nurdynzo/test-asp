using System.Linq;
using Abp.Dependency;

namespace Plateaumed.EHR.Patients.Abstractions;

public interface IPatientBaseQuery : ITransientDependency
{
    IQueryable<Patient> GetPatientByCodeBaseQuery(string patientCode);
}