using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Diagnoses.Abstraction
{
    public interface ICreateDiagnosisCommandHandler : ITransientDependency
    {
        Task<Diagnosis> Handle(Diagnosis diagnosis);

    }
}
