using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Query;

public class GetNextAppointmentByIdQueryHandler : IGetNextAppointmentByIdQueryhandler
{
    private readonly INextAppointmentBaseQuery _baseQuery;

    public GetNextAppointmentByIdQueryHandler(INextAppointmentBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<NextAppointmentReturnDto> Handle(long nextAppointmentId)
    {
        if (nextAppointmentId == 0)
            throw new UserFriendlyException($"Next Appointment Id is required.");


        var result = await _baseQuery.GetNextAppointmentById(nextAppointmentId);

        if (result == null)
            throw new UserFriendlyException($"No next appointment found.");

        return result;
    }
}
