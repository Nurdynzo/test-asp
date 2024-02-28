using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface ISavePatientFamilyHistoryCommandHandler : ITransientDependency
    {
        Task<PatientFamilyHistoryDto> Handle(PatientFamilyHistoryDto request);
    }
}
