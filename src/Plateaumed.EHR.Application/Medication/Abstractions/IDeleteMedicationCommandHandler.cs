using System.Threading.Tasks;
using Abp.Dependency;
namespace Plateaumed.EHR.Medication.Abstractions
{
    public interface IDeleteMedicationCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
