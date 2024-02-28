using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IGetPatientBloodTransfusionHistory : ITransientDependency
    {
        Task<List<GetPatientBloodTransfusionHistoryResponseDto>> Handle(long patientId);
    }
}
