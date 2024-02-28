using Plateaumed.EHR.PatientAppointments.Dtos;
using Plateaumed.EHR.PatientAppointments.Query.BaseQueryHelper;
using System.Linq;

namespace Plateaumed.EHR.PatientAppointments.Query.Common
{
    /// <summary>
    /// QueryExtension class
    /// </summary>
    public static class QueryExtension
    {
        /// <summary>
        /// An extension method for appointment projection
        /// </summary>
        /// <param name="query"></param>
        /// <param name="includePatientDetails"></param>
        /// <returns></returns>
        public static IQueryable<AppointmentListResponse> ToAppointmentListResponse(this IQueryable<AppointmentBaseQuery> query, bool includePatientDetails = true) {

            // Apply Projection
            var resultQuery = query.Select(x => new AppointmentListResponse
            {
                AttendingClinic = x.AttendingClinic != null
                    ? new AttendingClinicDto
                    {
                        DisplayName = x.AttendingClinic.DisplayName,
                        Id = x.AttendingClinic.Id
                    }
                    : null,
                AttendingPhysician = x.AttendingPhysician != null
                    ? new AttendingPhysicianDto
                    {
                        StaffCode = x.AttendingPhysician.StaffCode,
                        Id = x.AttendingPhysician.Id,
                        FullName =  x.StaffUser != null ? $"{x.StaffUser.DisplayName}": string.Empty,
                        Roles = x.Roles != null ? x.Roles.ToList() : null
                    }
                    : null,
                Type = x.Appointment.Type,
                Status = x.Appointment.Status,
                RepeatType = x.Appointment.RepeatType,
                Notes = x.Appointment.Notes,
                IsRepeat = x.Appointment.IsRepeat,
                StartTime = x.Appointment.StartTime,
                Duration = x.Appointment.Duration,
                Title = x.Appointment.Title,
                Id = x.Appointment.Id,
                AppointmentPatient = includePatientDetails ? new AppointmentPatientDto
                {
                    PatientCode = x.PatientCodeMapping.PatientCode,
                    FullName = x.Patient.FullName,
                    Id = x.Patient.Id,
                    GenderType = x.Patient.GenderType,
                    DateOfBirth = x.Patient.DateOfBirth,
                    PictureUrl = x.Patient.PictureUrl,
                    IsNewToHospital = x.Patient.IsNewToHospital
                } : null,
                ReferralDocument = x.ReferralDocument != null
                    ? new ReferralDocumentDto
                    {
                        Id = x.ReferralDocument.Id,
                        ReferralDocument = x.ReferralDocument.ReferralDocument,
                        DiagnosisSummary = x.ReferralDocument.DiagnosisSummary,
                        ReferringHealthCareProvider = x.ReferralDocument.ReferringHealthCareProvider
                    }
                    : null,
                ReferringClinic = x.ReferringClinic != null ? x.ReferringClinic.DisplayName : "",
                ScannedDocument = x.PatientScanDocument != null ? new ScannedDocumentDto
                {
                    Id = x.PatientScanDocument.Id,
                    IsApproved = x.PatientScanDocument.IsApproved,
                    ReviewerId = x.PatientScanDocument.ReviewerId
                } : null
            });

            return resultQuery;

        }
    }
}
