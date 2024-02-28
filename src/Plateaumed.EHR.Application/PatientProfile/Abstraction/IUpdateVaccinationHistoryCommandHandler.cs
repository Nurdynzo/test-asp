using Abp.Dependency;
using Plateaumed.EHR.Vaccines.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IUpdateVaccinationHistoryCommandHandler : ITransientDependency
    {
        Task Handle(CreateVaccinationHistoryDto request);
    }
}
