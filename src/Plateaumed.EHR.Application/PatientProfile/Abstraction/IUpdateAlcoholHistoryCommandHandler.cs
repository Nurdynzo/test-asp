using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IUpdateAlcoholHistoryCommandHandler : ITransientDependency
    {
        Task Handle(CreateAlcoholHistoryRequestDto request);
    }
}
