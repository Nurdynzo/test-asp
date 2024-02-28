using System.Threading.Tasks;
using Abp.Dependency;
namespace Plateaumed.EHR.Vaccines.Abstractions
{
    public interface IDeletePatientVaccinationCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
