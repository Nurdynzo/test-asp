using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface ICreateBloodTransfusionHistoryHandler : ITransientDependency
    {
        Task Handle(CreateBloodTransfusionHistoryRequestDto request);
    }
}
