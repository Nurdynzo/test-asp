using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.NextAppointments.Handlers;
public class EditNextAppointmentCommandHandler : IEditNextAppointmentCommandHandler
{
    private readonly IRepository<PatientEncounter, long> _encounterRepository;
    private readonly INextAppointmentBaseQuery _baseQuery;
    private readonly PatientAppointments.Abstractions.IUpdateAppointmentCommandHandler _editAppointmentCommandHandler;



    public EditNextAppointmentCommandHandler(
            IRepository<PatientEncounter, long> encounterRepository,
            INextAppointmentBaseQuery baseQuery,
            PatientAppointments.Abstractions.IUpdateAppointmentCommandHandler editAppointmentCommandHandler)
    {
        _baseQuery = baseQuery;
        _editAppointmentCommandHandler = editAppointmentCommandHandler;
        _encounterRepository = encounterRepository;
    }
    public async Task<CreateNextAppointmentDto> Handle(CreateNextAppointmentDto requestDto, long loginUserId)
    {
        if (requestDto.Id <= 0)
            throw new UserFriendlyException($"Appointment Id is required.");

        if (requestDto.PatientId <= 0)
            throw new UserFriendlyException($"PatientId is required.");

        if (requestDto.UnitId <= 0)
            throw new UserFriendlyException($"Please supply a valid unit");

        var operatingTimes = await _baseQuery.GetOperationUnitTime(requestDto.UnitId) ?? throw new UserFriendlyException($"No operation time found.");
        if (requestDto.DateType == DateTypes.Day || requestDto.DateType == DateTypes.Week || requestDto.DateType == DateTypes.Month)
        {
            if (requestDto.SeenIn == 0)
                throw new UserFriendlyException($"Please enter a number (day) indicating when the next appointment will hold.");

            var possibleDate = requestDto.DateType == DateTypes.Day ? DateTime.Now.Date.AddDays(requestDto.SeenIn.GetValueOrDefault()) :
                           requestDto.DateType == DateTypes.Week ? DateTime.Now.Date.AddDays(7 * requestDto.SeenIn.GetValueOrDefault()) :
                           DateTime.Now.Date.AddMonths(requestDto.SeenIn.GetValueOrDefault());
            requestDto.AppointmentDate = GenerateAppointmentDate(possibleDate, operatingTimes);
        }
        else
            requestDto.AppointmentDate = requestDto.AppointmentDate ?? throw new UserFriendlyException($"Appointment date is required.");

        var patientEncounter = await _encounterRepository.GetAll().Include(x => x.Appointment).Where(s => s.PatientId == requestDto.PatientId &&
                                   (s.Appointment.Status != AppointmentStatusType.Missed || s.Appointment.Status != AppointmentStatusType.Executed
                                   || s.Appointment.Status != AppointmentStatusType.Not_Arrived))
                                   .OrderByDescending(x => x.Id).FirstOrDefaultAsync();

        var staff = await _baseQuery.GetStaffFacilities(loginUserId, (long)patientEncounter.FacilityId);

        var patientAppointmentCreation = await _editAppointmentCommandHandler.Handle(new CreateOrEditPatientAppointmentDto()
        {
            Id = requestDto.Id,
            Title = $"Patient Next Appointment",
            Duration = 0,
            StartTime = requestDto.AppointmentDate.GetValueOrDefault(),
            IsRepeat = false,
            Notes = "",
            RepeatType = AppointmentRepeatType.Custom,
            Status = AppointmentStatusType.Rescheduled,
            Type = AppointmentType.Consultation,
            PatientId = requestDto.PatientId,
            AttendingPhysicianId = staff.StaffMemberId,
            AttendingClinicId = requestDto.UnitId,
            ReferringClinicId = patientEncounter.UnitId,
        });

        requestDto.Id = patientAppointmentCreation.Id.GetValueOrDefault();
        return requestDto;
    }

    private DateTime GenerateAppointmentDate(DateTime possibleAppointmentDate, List<OperationTimeDto> operatingTime)
    {
        var possible_dayOfWeek = (int)possibleAppointmentDate.DayOfWeek;
        var nextOperationTime = operatingTime.Where(d => possible_dayOfWeek <= d.DayOfTheWeekNo)
                                    .OrderBy(o => o.DayOfTheWeekNo).FirstOrDefault();
        nextOperationTime = nextOperationTime == null ? operatingTime.OrderBy(o => o.DayOfTheWeekNo).FirstOrDefault() : nextOperationTime;
        var operating_dayOfWeek = nextOperationTime.DayOfTheWeekNo;
        var aTime_Hour = nextOperationTime.OpeningTime.Value.Hour;
        var aTime_Min = nextOperationTime.OpeningTime.Value.Minute;
        var appointmentDate = possibleAppointmentDate.AddDays(operating_dayOfWeek - possible_dayOfWeek);
        appointmentDate = appointmentDate.AddHours(aTime_Hour);
        appointmentDate = appointmentDate.AddMinutes(aTime_Min);

        return appointmentDate;
    }
}
