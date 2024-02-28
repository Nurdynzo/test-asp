using System;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientAppointments.Dtos;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Query.Common;
using IBaseQuery = Plateaumed.EHR.PatientAppointments.Abstractions.IBaseQuery;

namespace Plateaumed.EHR.PatientAppointments.Query;

/// <summary>
/// 
/// </summary>
public class GetAppointmentForTodayQueryHandler : IGetAppointmentForTodayQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IGetCurrentUserFacilityIdQueryHandler _getCurrentUserFacilityId;

    /// <summary>
    /// Constructor for GetAppointmentForTodayQueryHandler
    /// </summary>
    /// <param name="baseQuery"></param>
    public GetAppointmentForTodayQueryHandler(IBaseQuery baseQuery, IGetCurrentUserFacilityIdQueryHandler getCurrentUserFacilityId)
    {
        _baseQuery = baseQuery;
        _getCurrentUserFacilityId = getCurrentUserFacilityId;
    }

    /// <summary>
    /// Handle method for GetAppointmentForTodayQueryHandler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<AppointmentListResponse>> Handle(GetAppointmentForTodayQueryRequest request)
    {
        var today = DateTime.Now.Date;
        var query = _baseQuery.GetAppointmentsBaseQuery();
        var facilityId = request.FacilityId ?? await _getCurrentUserFacilityId.Handle();

        // Apply Filtering
        query = request switch
        {
            { StartTime: not null , EndTime: not null } => query.Where(a => a.Appointment.StartTime.Date >= request.StartTime!.Value.Date && a.Appointment.StartTime.Date <= request.EndTime!.Value.Date),
            { StartTime: not null } => query.Where(a => a.Appointment.StartTime.Date == request.StartTime!.Value.Date),
            { EndTime: not null } => query.Where(a => a.Appointment.StartTime.Date >= today && a.Appointment.StartTime.Date <= request.EndTime!.Value.Date),
            _ => query.Where(a => a.Appointment.StartTime.Date == today)
        };
        query = request switch
        {
            { AttendingClinic: not null } => query.Where(a => a.AttendingClinic.DisplayName.Contains(request.AttendingClinic)),
            { AttendingPhysicianStaffCode: not null } => query.Where(a => a.AttendingPhysician.StaffCode.Contains(request.AttendingPhysicianStaffCode)),
            { PatientCode: not null } => query.Where(a => a.PatientCodeMapping.PatientCode.Contains(request.PatientCode)),
            { Status: not null } => query.Where(a => a.Appointment.Status.ToString().Contains(request.Status)),
            { AppointmentType: not null } => query.Where(a => a.Appointment.Type.ToString().Contains(request.AppointmentType)),
            _ => query
        };
        
        query = query.WhereIf(facilityId.HasValue, a => a.AttendingClinic.FacilityId == facilityId);
        
        var resultQuery = query.ToAppointmentListResponse()
            .GroupBy(x=>x.AppointmentPatient.Id)
            .Select(x=> new AppointmentListResponse
        {
            AttendingClinic = x.FirstOrDefault().AttendingClinic,
            AttendingPhysician = x.FirstOrDefault().AttendingPhysician,
            Type = x.FirstOrDefault().Type,
            Status = x.FirstOrDefault().Status,
            RepeatType = x.FirstOrDefault().RepeatType,
            Notes = x.FirstOrDefault().Notes,
            IsRepeat = x.FirstOrDefault().IsRepeat,
            StartTime = x.FirstOrDefault().StartTime,
            Duration = x.FirstOrDefault().Duration,
            Title = x.FirstOrDefault().Title,
            Id = x.FirstOrDefault().Id,
            AppointmentPatient = x.FirstOrDefault().AppointmentPatient,
            ReferralDocument = x.FirstOrDefault().ReferralDocument,
            ReferringClinic = x.FirstOrDefault().ReferringClinic,
            AppointmentCount = x.Count(),
            ScannedDocument = x.FirstOrDefault().ScannedDocument
        });

        // Apply Sorting, Skip, and Take
        var sortedQuery = resultQuery.OrderBy(request.Sorting ?? "StartTime desc");
        var pagedQuery = sortedQuery.Skip(request.SkipCount).Take(request.MaxResultCount);

        // Execute the query and retrieve the results
        var results = await pagedQuery.ToListAsync();
        var totalCount = await resultQuery.CountAsync();
        return new PagedResultDto<AppointmentListResponse>(totalCount, results);
    }
}