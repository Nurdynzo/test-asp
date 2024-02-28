using System.Linq;
using Abp.Dependency;

namespace Plateaumed.EHR.Diagnoses.Abstraction
{
    public interface IBaseQuery : ITransientDependency
    {
        public IQueryable<Diagnosis> GetPatientDiagnosisBaseQuery(int PatientId);
        IQueryable<Diagnosis> GetPatientDiagnosisWithEncounterId(long PatientId, long encounterId);

    }
}
