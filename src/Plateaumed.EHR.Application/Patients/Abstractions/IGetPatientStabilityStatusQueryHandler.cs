using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Abstractions
{
    public interface IGetPatientStabilityStatusQueryHandler: ITransientDependency
    {
        Task<List<PatientStabilityResponseDto>> Handle(long patientId, long encounterId);
    }
}

