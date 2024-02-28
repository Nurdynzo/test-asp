using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Dtos;
using Plateaumed.EHR.PatientAppointments.Query.Common;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientAppointments.Query;

/// <summary>
/// Handler for Getting list of appointments by patient id
/// </summary>
public class GetAppointmentByPatientIdQueryHandler : IGetAppointmentByPatientIdQueryHandler
{
    private readonly IBaseQuery _baseQuery;

    /// <summary>
    /// constructor for GetAppointmentByPatientIdQueryHandler
    /// </summary>
    /// <param name="baseQuery"></param>
    public GetAppointmentByPatientIdQueryHandler(IBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }


    /// <inheritdoc />
    public async Task<PagedResultDto<AppointmentListResponse>> Handle(GetAppointmentsByPatientIdRequest request)
    {


        if (request.StartDate != null && request.EndDate != null)
        {
            return await GetAppointmentsFilteredByDate(request);
        }
        else
        {
            return await GetAppointments(request);

        }

    }

    private async Task<PagedResultDto<AppointmentListResponse>> GetAppointmentsFilteredByDate(GetAppointmentsByPatientIdRequest request)
    {

        var query = _baseQuery.GetAppointmentsBaseQuery()
            .Where(a => a.Patient.Id == request.PatientId)
        .ToAppointmentListResponse(includePatientDetails: false);

        query = query
            .Where(a => a.StartTime.Date >= request.StartDate!.Value.Date
            && a.StartTime.Date <= request.EndDate!.Value.Date).OrderBy(a => a.StartTime);

        // Apply Skip and Take
        var pagedAppointmentsQuery = query.Skip(request.SkipCount).Take(request.MaxResultCount);

        // Execute the query and retrieve the results
        var results = await pagedAppointmentsQuery.ToListAsync();
        var count = await query.CountAsync();

        return new PagedResultDto<AppointmentListResponse>(count, results);
    }


    private async Task<PagedResultDto<AppointmentListResponse>> GetAppointments(GetAppointmentsByPatientIdRequest request)
    {

        var query = await _baseQuery.GetAppointmentsBaseQuery()
            .Where(a => a.Patient.Id == request.PatientId)
            .ToAppointmentListResponse(includePatientDetails: false).ToListAsync();


        var appointmentsForToday = query
            .Where(a => a.StartTime.Date == DateTime.Today.Date)
            .OrderBy(a => a.StartTime);

        var upcomingAppointments = query
            .Where(a => a.StartTime.Date > DateTime.Today.Date)
            .OrderBy(a => a.StartTime);
        
        var pastAppointments = query
            .Where(a => a.StartTime.Date < DateTime.Today.Date)
            .OrderByDescending(a => a.StartTime);

        var orderedAppointments = new List<AppointmentListResponse>();

        orderedAppointments.AddRange(appointmentsForToday);

        orderedAppointments.AddRange(upcomingAppointments);

        orderedAppointments.AddRange(pastAppointments);

        // Apply Skip and Take
        var pageAppointments = orderedAppointments.Skip(request.SkipCount).Take(request.MaxResultCount);

        var results = pageAppointments.ToList();
        var count = orderedAppointments.Count();

        return new PagedResultDto<AppointmentListResponse>(count, results);
    }
}