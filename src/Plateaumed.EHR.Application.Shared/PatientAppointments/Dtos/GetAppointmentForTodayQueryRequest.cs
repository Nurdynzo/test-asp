using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.PatientAppointments.Dtos;

public class GetAppointmentForTodayQueryRequest: PagedAndSortedResultRequestDto
{
    public long? FacilityId { get; set; }

    public string PatientCode { get; set; }

    public string AppointmentType { get; set; } 

    public string Status { get; set; } 

    public string AttendingClinic { get; set; }

    public string AttendingPhysicianStaffCode { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}