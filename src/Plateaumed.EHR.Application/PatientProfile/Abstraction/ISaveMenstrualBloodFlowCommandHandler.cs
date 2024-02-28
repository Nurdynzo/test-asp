using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface ISaveMenstrualBloodFlowCommandHandler : ITransientDependency
    {
        Task<SaveMenstrualBloodFlowCommandRequest> Handle(SaveMenstrualBloodFlowCommandRequest request);
    }
}