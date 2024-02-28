using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeleteReviewOfSystemDataCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
