using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.PatientAppointments.Dtos;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientAppointments.Abstractions;

/// <inheritdoc />
public interface IGetAppointmentByPatientIdQueryHandler: ITransientDependency
{
    /// <summary>
    /// Handler to query appointments by patient id
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PagedResultDto<AppointmentListResponse>> Handle(GetAppointmentsByPatientIdRequest request);
}