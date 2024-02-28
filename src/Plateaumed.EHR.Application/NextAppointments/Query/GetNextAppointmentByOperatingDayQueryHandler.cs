using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Query;

public class GetDoctorAndPatientAppointmentQueryHandler : IGetDoctorAndPatientAppointmentQueryHandler
{
    private readonly INextAppointmentBaseQuery _baseQuery;

    public GetDoctorAndPatientAppointmentQueryHandler(INextAppointmentBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<List<NextAppointmentReturnDto>> Handle(long patientId, long doctorUserId)
    {
        if (patientId == 0)
            throw new UserFriendlyException($"Patient Id is required.");
        if (doctorUserId == 0)
            throw new UserFriendlyException($"Doctor Id is required.");
        var result = await _baseQuery.GetDoctorAndPatientAppointment(patientId, doctorUserId);

        if (result == null)
            throw new UserFriendlyException($"No next appointment found for the select day.");

        return result;
    }
}
