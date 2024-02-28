using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeleteBloodTransfusionHistoryCommandHandler : ITransientDependency
    {
        Task Handle(long historyId);
    }
}
