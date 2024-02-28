using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.PatientAppointments.Dtos;

namespace Plateaumed.EHR.PatientAppointments.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IGetAppointmentForTodayQueryHandler: ITransientDependency
{
    /// <summary>
    /// Handle method for GetAppointmentForTodayQueryHandler
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PagedResultDto<AppointmentListResponse>> Handle(GetAppointmentForTodayQueryRequest request);
}