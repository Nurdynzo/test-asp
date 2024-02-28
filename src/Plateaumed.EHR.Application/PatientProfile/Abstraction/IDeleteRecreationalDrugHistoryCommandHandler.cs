using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeleteRecreationalDrugHistoryCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
