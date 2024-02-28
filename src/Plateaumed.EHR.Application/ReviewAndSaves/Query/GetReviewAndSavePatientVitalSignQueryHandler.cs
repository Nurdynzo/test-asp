using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class GetReviewAndSavePatientVitalSignQueryHandler : IGetReviewAndSavePatientVitalSignQueryHandler
{
    private readonly IGetPatientVitalsSummaryQueryHandler _vitalSignQueryHandler;
    
    public GetReviewAndSavePatientVitalSignQueryHandler(IGetPatientVitalsSummaryQueryHandler vitalSignQueryHandler)
    {
        _vitalSignQueryHandler = vitalSignQueryHandler;
    }

    public async Task<List<PatientVitalsSummaryResponseDto>> Handle(long patientId, long encounterId)
    {
        //Get patient vital signs
        var vitalSign = await _vitalSignQueryHandler.Handle(patientId, encounterId: encounterId) ??
                        new List<PatientVitalsSummaryResponseDto>();

        return vitalSign;
    }
}
