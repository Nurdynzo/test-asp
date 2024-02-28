using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Query;

public class GetIntakeOutputSavedHistoryQueryHandler : IGetIntakeOutputSavedHistoryQueryHandler
{
    private readonly IBaseQuery _baseQuery;

    public GetIntakeOutputSavedHistoryQueryHandler(IBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<List<PatientIntakeOutputDto>> Handle(long patientId, long? procedureId = null)
    {
        if (patientId == 0)
            throw new UserFriendlyException($"Patient Id is required.");

        var result = await _baseQuery.GetPatientIntakeOutputHistory(patientId);
        return result;
    }
}
