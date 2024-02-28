using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.PatientAppointments.Dtos;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientAppointments;

public interface IAppointmentAppService : IApplicationService
{
    /// <summary>
    /// Get List of appointments for today in receptionist landing page
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<AppointmentListResponse>> GetAppointmentListForToday(GetAppointmentForTodayQueryRequest input);

    /// <summary>
    /// Get Most Recent Appointment for patient
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<AppointmentListResponse> GetMostRecentAppointmentForPatient(GetMostRecentAppointmentForPatientRequest request);

    /// <summary>
    /// Create or Update Patient Appointment
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<CreateOrEditPatientAppointmentDto> CreateOrEdit(CreateOrEditPatientAppointmentDto request);

    /// <summary>
    /// Delete Patient Appointment
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task Delete(EntityDto<long> request);

}