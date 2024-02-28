using System.Threading.Tasks;
using Abp.Dependency;
namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeletePatientMenstrualFlowCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
