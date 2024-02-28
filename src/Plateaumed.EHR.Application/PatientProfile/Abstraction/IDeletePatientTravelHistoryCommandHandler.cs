using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeletePatientTravelHistoryCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
