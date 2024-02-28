using System.Threading.Tasks;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientAppointments.Query.Common;

namespace Plateaumed.EHR.PatientAppointments.Query;
/// <summary>
/// Get Most Recent Appointment Handler
/// </summary>
public class GetMostRecentAppointmentQueryHandler : IGetMostRecentAppointmentQueryHandler
{
    private readonly IBaseQuery _baseQuery;

    /// <summary>
    /// constructor for GetMostRecentAppointmentQueryHandler
    /// </summary>
    /// <param name="baseQuery"></param>
    public GetMostRecentAppointmentQueryHandler(IBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    /// <inheritdoc />
    public async Task<AppointmentListResponse> Handle(GetMostRecentAppointmentForPatientRequest request)
    {
        var query = await _baseQuery.GetAppointmentsBaseQuery().ToAppointmentListResponse()
            .OrderByDescending(x=>x.StartTime).FirstOrDefaultAsync(x=>x.AppointmentPatient.Id == request.PatientId);
        return query;
        
    }
}