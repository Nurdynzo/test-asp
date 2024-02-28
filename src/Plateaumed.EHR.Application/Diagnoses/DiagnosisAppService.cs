using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Diagnoses.Abstraction;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses
{
    [AbpAuthorize(AppPermissions.Pages_Diagnosis)]
    public class DiagnosisAppService : EHRAppServiceBase, IDiagnosisAppService
    {
        private readonly ICreateDiagnosisCommandHandler _createDiagnosisCommand;
        private readonly IGetPatientDiagnosisQueryHandler _getPatientDaignosisQuery;
        private readonly IDeletePatientDiagnosisCommandHandler _deletePatientDiagnosisCommandHandler;
        private readonly IFormatDiagnosisHandler _daignosisRequestCommandHandler;

        public DiagnosisAppService(
            ICreateDiagnosisCommandHandler createDiagnosisCommand,
            IGetPatientDiagnosisQueryHandler getPatientDaignosisQuery,
            IDeletePatientDiagnosisCommandHandler deletePatientDiagnosisCommandHandler,
            IFormatDiagnosisHandler daignosisRequestCommandHandler
        )
        {
            _createDiagnosisCommand = createDiagnosisCommand;
            _getPatientDaignosisQuery = getPatientDaignosisQuery;
            _deletePatientDiagnosisCommandHandler = deletePatientDiagnosisCommandHandler;
            _daignosisRequestCommandHandler = daignosisRequestCommandHandler;
        }

        [AbpAuthorize(AppPermissions.Pages_Diagnosis_Create)]
        public async Task CreateDiagnosis(CreateDiagnosisDto createDiagnosis)
        {
            var combinedDiagnoses = _daignosisRequestCommandHandler.FormatDiagnoses(createDiagnosis);

            var diagnosis = ObjectMapper.Map<Diagnosis>(createDiagnosis);

            // Set the diagnosis description to the combined diagnoses text
            diagnosis.Description = combinedDiagnoses;
            // handle request
            await _createDiagnosisCommand.Handle(diagnosis);
        }
        public async Task DeleteDiagnosis(long diagnosisId) =>
            await _deletePatientDiagnosisCommandHandler.Handle(diagnosisId);

        public async Task<List<PatientDiagnosisReturnDto>> GetPatientDiagnosis(int patientId)
        {
            var diagnosis = await _getPatientDaignosisQuery.Handle(patientId);
            return diagnosis;
        }
    }
}
