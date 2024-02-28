using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeletePatientImplantCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
