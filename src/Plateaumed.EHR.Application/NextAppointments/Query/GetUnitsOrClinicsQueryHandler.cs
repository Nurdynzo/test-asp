using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Encounters.Abstractions;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Query;

public class GetUnitsOrClinicsQueryHandler : IGetUnitsOrClinicsQueryHandler
{
    private readonly INextAppointmentBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;
    private readonly IGetPatientEncounterQueryHandler _patientEncounterQueryhandler;
    
    public GetUnitsOrClinicsQueryHandler(INextAppointmentBaseQuery baseQuery, 
        IAbpSession abpSession,
        IGetPatientEncounterQueryHandler patientEncounterQueryhandler)
    {
        _baseQuery = baseQuery;
        _abpSession = abpSession;
        _patientEncounterQueryhandler = patientEncounterQueryhandler;
    }

    public async Task<List<NextAppointmentUnitReturnDto>> Handle(long userId, long facilityId, long encounterId)
    {
        var patientEncounter = await _patientEncounterQueryhandler.Handle(encounterId);

        if (patientEncounter.PatientId == 0)
            throw new UserFriendlyException("Patient Id is required.");
        if (userId == 0)
            throw new UserFriendlyException("Doctor User Id is required.");

        var result = await _baseQuery.GetAllPossibleUnitsAndClinics(userId, patientEncounter.PatientId, _abpSession.TenantId, facilityId, encounterId);

        if (result == null || result.Count <= 0)
            throw new UserFriendlyException("No unit found for this doctor.");

        return  result;
    }
}
