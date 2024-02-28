using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IGetPatientSurgicalHistoryQueryHandler : ITransientDependency
    {
        Task<List<GetSurgicalHistoryResponseDto>> Handle(long patientId);
    }
}
