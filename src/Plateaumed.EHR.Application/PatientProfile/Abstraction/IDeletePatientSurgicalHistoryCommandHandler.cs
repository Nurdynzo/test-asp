using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeletePatientSurgicalHistoryCommandHandler : ITransientDependency
    {
        Task Handle(long surigcalHistoryId);
    }
}
