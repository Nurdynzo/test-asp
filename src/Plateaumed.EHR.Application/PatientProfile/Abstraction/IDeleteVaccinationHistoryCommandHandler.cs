using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeleteVaccinationHistoryCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
