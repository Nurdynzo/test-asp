using System.Threading.Tasks;
using Abp.Dependency;
namespace Plateaumed.EHR.Diagnoses.Abstraction
{
    public interface IDeletePatientDiagnosisCommandHandler: ITransientDependency
    {
        Task Handle(long diagnosisId);
    }
}
