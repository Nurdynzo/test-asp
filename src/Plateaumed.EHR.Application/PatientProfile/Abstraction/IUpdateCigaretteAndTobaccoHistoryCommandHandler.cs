using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IUpdateCigaretteAndTobaccoHistoryCommandHandler : ITransientDependency
    {
        Task Handle(CreateCigaretteHistoryRequestDto request);
    }
}
