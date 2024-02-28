

using Plateaumed.EHR.Patients;
using System.Collections.Generic;
using System.Linq;
using System;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Tests.PatientAppointments.Util
{
    public static class CommonUtils
    {

        public static IQueryable<PatientAppointment> GetPatientAppointmentsWithinTheNextDayAsQueryable()
        {

            var startDay = DateTime.Now.AddDays(1).Date;
            var appointments = new List<PatientAppointment>();

            for (var index = 0; index < 7; index++)
            {

                startDay = startDay.AddHours(2.5);

                var newAppointment = new PatientAppointment()
                {
                    TenantId = 1,
                    Id = index + 1,
                    PatientId = 1,
                    Duration = 10,
                    StartTime = startDay,
                    Status = AppointmentStatusType.Not_Arrived,
                    Type = AppointmentType.Walk_In,
                    AttendingClinicId = index + 1,
                    AttendingPhysicianId = 1,
                    ReferringClinicId = 1,
                    PatientReferralDocumentId = 1,
                    Notes = $"Test Notes{index + 1}",
                    Title = $"Test Title{index + 1}",
                    IsRepeat = true,
                    RepeatType = AppointmentRepeatType.Daily,
                };

                appointments.Add(newAppointment);
            }

            return appointments.AsQueryable();
        }

        public static CreateOrEditPatientAppointmentDto GetCreateOrEditAppointmentRequest(
            DateTime StartTime, 
            long AppointmentId = 0,
            AppointmentType type = AppointmentType.Walk_In,
            long AttendingClinicId = 3)
        {

            return new CreateOrEditPatientAppointmentDto()
            {
                Id = AppointmentId,
                PatientId = 1,
                Duration = 10,
                StartTime = StartTime,
                Status = AppointmentStatusType.Not_Arrived,
                Type = type,
                AttendingClinicId = AttendingClinicId,
                AttendingPhysicianId = 1,
                ReferringClinicId = 1,
                PatientReferralId = 1,
                Notes = "Test Notes3",
                Title = "Test Title3",
                IsRepeat = false,
                RepeatType = AppointmentRepeatType.Daily,
            };
        }

        public static PatientAppointment GetPatientAppointment(
            AppointmentStatusType appointmentStatus,
            DateTime? StartTime = null, 
            long AppointmentId = 1)
        {
            return new PatientAppointment()
            {
                TenantId = 1,
                Id = AppointmentId,
                PatientId = 1,
                Duration = 10,
                StartTime = StartTime ?? DateTime.Now,
                Status = appointmentStatus,
                Type = AppointmentType.Walk_In,
                AttendingClinicId = 1,
                AttendingPhysicianId = 1,
                ReferringClinicId = 1,
                PatientReferralDocumentId = 1,
                Notes = "Test Notes",
                Title = "Test Title",
                IsRepeat = true,
                RepeatType = AppointmentRepeatType.Daily,

            };
        }
        
        public static PatientAppointment ConvertAppointmentRequestToPatientAppointment(
            CreateOrEditPatientAppointmentDto request, bool applyStartTime = true)
        {
            return new PatientAppointment()
            {
                TenantId = 1,
                Id = (long)request.Id,
                PatientId = (long)request.Id,
                Duration = request.Duration,
                StartTime = applyStartTime ? request.StartTime : DateTime.Now,
                Status = request.Status,
                Type = request.Type,
                AttendingClinicId = request.AttendingClinicId,
                AttendingPhysicianId = request.AttendingPhysicianId,
                ReferringClinicId = request.ReferringClinicId,
                PatientReferralDocumentId = request.PatientReferralId,
                Notes = request.Notes,
                Title = request.Title,
                IsRepeat = request.IsRepeat,
                RepeatType = request.RepeatType,

            };
        }

        public static IQueryable<PatientReferralDocument> GetReferralDocuments()
        {
            return new List<PatientReferralDocument>
            {
                new()
                {
                    Id = 1,
                    ReferringHealthCareProvider = "Test",
                    DiagnosisSummary = "Test Diagnosis",
                    ReferralDocument = Guid.NewGuid(),
                    PatientId = 1
                }
            }.AsQueryable();
        }
    }
}
