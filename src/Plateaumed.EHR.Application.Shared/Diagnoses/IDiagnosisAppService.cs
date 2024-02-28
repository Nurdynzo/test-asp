using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses
{
    public interface IDiagnosisAppService : IApplicationService
    {
        Task CreateDiagnosis(CreateDiagnosisDto createDiagnosis);

        Task<List<PatientDiagnosisReturnDto>> GetPatientDiagnosis(int patientId);
    }
}
