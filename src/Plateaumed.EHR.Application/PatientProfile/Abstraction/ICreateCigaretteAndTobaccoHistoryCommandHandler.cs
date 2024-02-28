using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface ICreateCigaretteAndTobaccoHistoryCommandHandler : ITransientDependency
    {
        Task Handle(CreateCigaretteHistoryRequestDto request);
    }
}
