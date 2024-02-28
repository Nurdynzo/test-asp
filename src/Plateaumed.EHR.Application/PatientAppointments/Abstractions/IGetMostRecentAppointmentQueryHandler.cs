using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientAppointments.Dtos;

namespace Plateaumed.EHR.PatientAppointments.Abstractions;

/// <summary>
/// Get Most Recent Appointment
/// </summary>
public interface IGetMostRecentAppointmentQueryHandler : ITransientDependency
{
    /// <summary>
    /// Handle method for GetMostRecentAppointmentQueryHandler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<AppointmentListResponse> Handle(GetMostRecentAppointmentForPatientRequest request);
}