using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Patients;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Plateaumed.EHR.PatientAppointments.Utils
{
    /// <summary>
    /// A class to access validation common to two or more handlers
    /// </summary>
    public static class CommonAppointmentValidation
    {
        /// <summary>
        /// A method to that contains common validation for creating or editing an appointment
        /// </summary>
        /// <param name="patientAppointmentRepository"></param>
        /// <param name="appointment"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public static async Task CheckIfAppointmentCreateOrEditRequestIsValid(IRepository<PatientAppointment, long> patientAppointmentRepository,  PatientAppointment appointment)
        {

            if (appointment.StartTime < DateTime.Now)
            {
                throw new UserFriendlyException("Appointment startime cannot be set to a time in the past.");
            }

            // Get all appointments for a particular date, but if updating exclude appointment to be edited by checking appointmentId.
            var isExist = await patientAppointmentRepository.GetAll()
                .Where(x => x.PatientId == appointment.PatientId &&
                            x.StartTime.Date == appointment.StartTime.Date && x.Id != appointment.Id).OrderBy(x => x.StartTime).ToListAsync();

            if (isExist.Count <= 0)
            {
                return;
            }

            var appointmentAlreadyExistInClinic = isExist.Any(x => x.AttendingClinicId == appointment.AttendingClinicId
            && x.StartTime.Date == appointment.StartTime.Date);

            if (appointmentAlreadyExistInClinic)
            {

                throw new UserFriendlyException("Appointment already exist for this patient on this date in the same clinic.");

            }

            if (ValidateAppointmentDoesNotExistWithin2hrsOfExistingAppointment(isExist, appointment))
            {

                var actionString = appointment.Id <= 0 ? "created" : "updated";

                throw new UserFriendlyException($"Appointment cannot be {actionString} within 2hrs of existing appointment on same day");
            }

        }



        private static bool ValidateAppointmentDoesNotExistWithin2hrsOfExistingAppointment(List<PatientAppointment> existingAppointments, PatientAppointment newAppointment)
        {

            var appointmentStartTimeIs2hoursGreater = true;

            for (var index = 0; index < existingAppointments.Count; index++)
            {

                var existingAppointment = existingAppointments[index];
                var previousAppointment = index > 0 ? existingAppointments[index - 1] : null;

                if (existingAppointment.StartTime > newAppointment.StartTime)
                {

                    if (newAppointment.StartTime.AddHours(2) < existingAppointment.StartTime)
                    {

                        if (previousAppointment != null && newAppointment.StartTime.AddHours(-2) <= previousAppointment.StartTime)
                        {

                            appointmentStartTimeIs2hoursGreater = true;
                            break;
                        }

                        appointmentStartTimeIs2hoursGreater = false;
                        break;
                    }
                    else
                    {
                        appointmentStartTimeIs2hoursGreater = true;
                        break;
                    }
                }
                else
                {
                    appointmentStartTimeIs2hoursGreater = false;
                    continue;
                }

            }

            return appointmentStartTimeIs2hoursGreater;
        }
    }
}
