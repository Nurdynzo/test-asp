using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Dtos;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientAppointments;

/// <inheritdoc cref="Plateaumed.EHR.EHRAppServiceBase" />

[AbpAuthorize(AppPermissions.Pages_PatientAppointments)]
public class AppointmentAppService: EHRAppServiceBase, IAppointmentAppService
{
    private readonly IGetAppointmentForTodayQueryHandler _getAppointmentForTodayQueryHandler;
    private readonly IGetMostRecentAppointmentQueryHandler _getMostRecentAppointmentQueryHandler;
    private readonly IDeleteAppointmentCommandHandler _deleteAppointmentCommandHandler;
    private readonly ICreateAppointmentCommandHandler _createAppointmentCommandHandler;
    private readonly IUpdateAppointmentCommandHandler _updateAppointmentCommandHandler;


    /// <inheritdoc />
    public AppointmentAppService(
        IGetAppointmentForTodayQueryHandler getAppointmentForTodayQueryHandler,
        IGetMostRecentAppointmentQueryHandler getMostRecentAppointmentQueryHandler,
        IDeleteAppointmentCommandHandler deleteAppointmentCommandHandler,
        ICreateAppointmentCommandHandler createAppointmentCommandHandler,
        IUpdateAppointmentCommandHandler updateAppointmentCommandHandler)
    {
        _getAppointmentForTodayQueryHandler = getAppointmentForTodayQueryHandler;
        _getMostRecentAppointmentQueryHandler = getMostRecentAppointmentQueryHandler;
        _deleteAppointmentCommandHandler = deleteAppointmentCommandHandler;
        _createAppointmentCommandHandler = createAppointmentCommandHandler;
        _updateAppointmentCommandHandler = updateAppointmentCommandHandler;
    }

    /// <inheritdoc />
    public Task<PagedResultDto<AppointmentListResponse>> GetAppointmentListForToday(GetAppointmentForTodayQueryRequest input)
    {
        return _getAppointmentForTodayQueryHandler.Handle(input);
    }

    /// <inheritdoc />
    public async Task<AppointmentListResponse> GetMostRecentAppointmentForPatient(GetMostRecentAppointmentForPatientRequest request)
    => await _getMostRecentAppointmentQueryHandler.Handle(request);

    /// <inheritdoc/>
    public async Task<CreateOrEditPatientAppointmentDto> CreateOrEdit(CreateOrEditPatientAppointmentDto request){

        if (request.Id is null or <= 0) {
            return await _createAppointmentCommandHandler.Handle(request, AbpSession.TenantId.GetValueOrDefault());
        }

        return await _updateAppointmentCommandHandler.Handle(request);
    }

    /// <inheritdoc />
    public async Task Delete(EntityDto<long> request) => await _deleteAppointmentCommandHandler.Handle(request);
}