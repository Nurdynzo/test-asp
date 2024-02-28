using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;

namespace Plateaumed.EHR.Discharges.Query;

public class GetPatientDischargeWithEncounterIdQueryHandler : IGetPatientDischargeWithEncounterIdQueryHandler
{
    private readonly IDischargeBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;

    public GetPatientDischargeWithEncounterIdQueryHandler(IDischargeBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<DischargeDto> Handle(long patientId, long encounterId)
    {
        if (patientId == 0)
            throw new UserFriendlyException("Patient Id is required.");
        if (encounterId == 0)
            throw new UserFriendlyException("Encounter Id is required.");

        var discharges = await _baseQuery.GetPatientDischargeWithEncounterId(patientId, encounterId);

        return discharges;
    }
}