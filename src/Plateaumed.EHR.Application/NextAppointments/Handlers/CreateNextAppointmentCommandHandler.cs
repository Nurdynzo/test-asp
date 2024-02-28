using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.NextAppointments.Handlers;

public class CreateNextAppointmentCommandHandler : ICreateNextAppointmentCommandHandler
{
    private readonly ICreateAppointmentCommandHandler _createAppointmentCommandHandler;
    private readonly IRepository<PatientEncounter, long> _encounterRepository;
    private readonly IAbpSession _abpSession;
    private readonly INextAppointmentBaseQuery _baseQuery;

    public CreateNextAppointmentCommandHandler(
        IRepository<PatientEncounter, long> encounterRepository,
        ICreateAppointmentCommandHandler createAppointmentCommandHandler,
        INextAppointmentBaseQuery baseQuery,
        IAbpSession abpSession)
    {
        _encounterRepository = encounterRepository;
        _createAppointmentCommandHandler = createAppointmentCommandHandler;
        _baseQuery = baseQuery;
        _abpSession = abpSession;
    }

    public async Task<CreateNextAppointmentDto> Handle(CreateNextAppointmentDto requestDto, long loginUserId)
    {
        if (requestDto.PatientId <= 0)
            throw new UserFriendlyException("PatientId is required.");

        if (requestDto.UnitId <= 0)
            throw new UserFriendlyException("Please supply a valid unit");

        var operatingTimes = await _baseQuery.GetOperationUnitTime(requestDto.UnitId) ??
                             throw new UserFriendlyException("No operation time found.");
        if (requestDto.DateType is DateTypes.Day or DateTypes.Week or DateTypes.Month)
        {
            if (requestDto.SeenIn == 0)
                throw new UserFriendlyException(
                    "Please enter a number (day) indicating when the appointment will hold.");

            var possibleDate = requestDto.DateType switch
            {
                DateTypes.Day => DateTime.Now.Date.AddDays(requestDto.SeenIn.GetValueOrDefault()),
                DateTypes.Week => DateTime.Now.Date.AddDays(7 * requestDto.SeenIn.GetValueOrDefault()),
                _ => DateTime.Now.Date.AddMonths(requestDto.SeenIn.GetValueOrDefault())
            };
            requestDto.AppointmentDate = GenerateAppointmentDate(possibleDate, operatingTimes);
        }
        else
            requestDto.AppointmentDate = requestDto.AppointmentDate ??
                                         throw new UserFriendlyException("Appointment date is required.");

        var patientEncounter = _encounterRepository.GetAll().FirstOrDefault(s => s.Id == requestDto.EncounterId);
        var staff = await _baseQuery.GetStaffFacilities(loginUserId, (long)patientEncounter.FacilityId);

        var patientAppointmentCreation = await _createAppointmentCommandHandler.Handle(
            new CreateOrEditPatientAppointmentDto
            {
                Id = requestDto.Id.GetValueOrDefault(),
                Title = "Patient Next Appointment",
                Duration = 10,
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
            }, _abpSession.TenantId.GetValueOrDefault());

        requestDto.Id = patientAppointmentCreation.Id.GetValueOrDefault();
        return requestDto;
    }

    private DateTime GenerateAppointmentDate(DateTime possibleAppointmentDate, List<OperationTimeDto> operatingTime)
    {
        if (operatingTime == null || operatingTime.Count == 0)
            return possibleAppointmentDate;

        var possibleDayOfWeek = (int)possibleAppointmentDate.DayOfWeek;
        var nextOperationTime =
            operatingTime
                .Where(d => possibleDayOfWeek <= d.DayOfTheWeekNo)
                .MinBy(o => o.DayOfTheWeekNo);
        nextOperationTime ??= operatingTime.MinBy(o => o.DayOfTheWeekNo);

        var operatingDayOfWeek = nextOperationTime?.DayOfTheWeekNo ?? 0;
        var aTimeHour = nextOperationTime.OpeningTime.Value.Hour;
        var aTimeMin = nextOperationTime.OpeningTime.Value.Minute;
        var appointmentDate = possibleAppointmentDate.AddDays(operatingDayOfWeek - possibleDayOfWeek);
        appointmentDate = appointmentDate.AddHours(aTimeHour);
        appointmentDate = appointmentDate.AddMinutes(aTimeMin);
        return appointmentDate;
    }
}
