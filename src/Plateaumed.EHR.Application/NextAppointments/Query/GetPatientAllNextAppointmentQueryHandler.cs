using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Query;

public class GetPatientAllNextAppointmentQueryHandler : IGetPatientAllNextAppointmentQueryHandler
{
    private readonly INextAppointmentBaseQuery _baseQuery;

    public GetPatientAllNextAppointmentQueryHandler(INextAppointmentBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<List<NextAppointmentReturnDto>> Handle(long patientId)
    {
        if (patientId == 0)
            throw new UserFriendlyException("Patient Id is required.");

        var result = await _baseQuery.GetNextAppointmentByPatientId(patientId);

        if (result == null)
            throw new UserFriendlyException("No patient next appointment found.");

        return result;
    }
}