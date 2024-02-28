using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns
{
    public class VitalSignsAppService : EHRAppServiceBase, IVitalSignsAppService
    {
        private readonly IGetAllVitalSignsQueryHandler _getAllVitalSigns;
        private readonly IGetGcsScoringQueryHandler _getGcsScoring;
        private readonly IGetApgarScoringQueryHandler _getApgarScoring;
        private readonly ICreatePatientVitalCommandHandler _createPatientVitalCommandHandler;
        private readonly IGetPatientVitalsQueryHandler _getPatientVitalsQueryHandler;
        private readonly IDeletePatientVitalCommandHandler _deletePatientVitalCommandHandler;
        private readonly IRecheckPatientVitalCommandHandler _recheckPatientVitalCommandHandler; 
        private readonly IGetPatientVitalsSummaryQueryHandler _getPatientVitalsSummaryQueryHandler;

        public VitalSignsAppService(IGetAllVitalSignsQueryHandler getAllVitalSigns,
            IGetGcsScoringQueryHandler getGcsScoring,
            IGetApgarScoringQueryHandler getApgarScoring, ICreatePatientVitalCommandHandler createPatientVitalCommandHandler, IGetPatientVitalsQueryHandler getPatientVitalsQueryHandler, IDeletePatientVitalCommandHandler deletePatientVitalCommandHandler, IRecheckPatientVitalCommandHandler recheckPatientVitalCommandHandler, IGetPatientVitalsSummaryQueryHandler getPatientVitalsSummaryQueryHandler)
        {
            _getAllVitalSigns = getAllVitalSigns;
            _getGcsScoring = getGcsScoring;
            _getApgarScoring = getApgarScoring;
            _createPatientVitalCommandHandler = createPatientVitalCommandHandler;
            _getPatientVitalsQueryHandler = getPatientVitalsQueryHandler;
            _deletePatientVitalCommandHandler = deletePatientVitalCommandHandler;
            _recheckPatientVitalCommandHandler = recheckPatientVitalCommandHandler; 
            _getPatientVitalsSummaryQueryHandler = getPatientVitalsSummaryQueryHandler;
        }

        public async Task<List<GetAllVitalSignsResponse>> GetAll() =>
            await _getAllVitalSigns.Handle();

        public async Task CreatePatientVital(CreateMultiplePatientVitalDto input) =>
            await _createPatientVitalCommandHandler.Handle(input);
        
        public async Task<List<PatientVitalsSummaryResponseDto>> GetPatientVitalsSummary(long patientId, long? procedureId = null) 
            => await _getPatientVitalsSummaryQueryHandler.Handle(patientId, procedureId);
        
        public async Task<List<PatientVitalResponseDto>> GetPatientVitals(long patientId, long? procedureId = null) 
            => await _getPatientVitalsQueryHandler.Handle(patientId, procedureId);

        public async Task RecheckPatientVital(RecheckPatientVitalDto input) =>
            await _recheckPatientVitalCommandHandler.Handle(input);

        public async Task DeletePatientVital(List<long> patientVitalIds)
            => await _deletePatientVitalCommandHandler.Handle(patientVitalIds);
        
        public async Task<List<GetGCSScoringResponse>> GetGCSScoring(long patientId) => await _getGcsScoring.Handle(patientId);

        public async Task<List<GetApgarScoringResponse>> GetApgarScoring()
        {
            return await _getApgarScoring.Handle();
        }
    }
}
