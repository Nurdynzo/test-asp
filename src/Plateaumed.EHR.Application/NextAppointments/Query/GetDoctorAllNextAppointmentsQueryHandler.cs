using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Query;

public class GetDoctorAllNextAppointmentsQueryHandler : IGetDoctorAllNextAppointmentsQueryHandler
{
    private readonly INextAppointmentBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;

    public GetDoctorAllNextAppointmentsQueryHandler(INextAppointmentBaseQuery baseQuery, IAbpSession abpSession)
    {
        _baseQuery = baseQuery;
        _abpSession = abpSession;
    }

    public async Task<List<NextAppointmentReturnDto>> Handle(long userId)
    {
        if (userId == 0)
            throw new UserFriendlyException($"Doctor user Id is required.");

        var result = await _baseQuery.GetNextAppointmentByDoctorId(userId);

        if (result == null)
            throw new UserFriendlyException($"No doctor next appointment found.");

        return result;
    }
}
