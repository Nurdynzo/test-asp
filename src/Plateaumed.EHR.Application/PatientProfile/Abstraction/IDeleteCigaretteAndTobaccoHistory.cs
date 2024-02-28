using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeleteCigaretteAndTobaccoHistory : ITransientDependency
    {
        Task Handle(long id);
    }
}
