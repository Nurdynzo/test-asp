using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.BedMaking.Abstractions;
using Plateaumed.EHR.BedMaking.Dtos;

namespace Plateaumed.EHR.BedMaking;

[AbpAuthorize(AppPermissions.Pages_BedMaking)]
public class BedMakingAppService : EHRAppServiceBase, IBedMakingAppService
{
    private readonly ICreateBedMakingCommandHandler _createBedMakingCommandHandler;
    private readonly IGetPatientBedMakingSummaryQueryHandler _getPatientBedMakingSummaryQuery;
    private readonly IDeleteBedMakingCommandHandler _deleteBedMakingCommandHandler;
    
    public BedMakingAppService(ICreateBedMakingCommandHandler createBedMakingCommandHandler, IGetPatientBedMakingSummaryQueryHandler getPatientBedMakingSummaryQuery, IDeleteBedMakingCommandHandler deleteBedMakingCommandHandler)
    {
        _createBedMakingCommandHandler = createBedMakingCommandHandler;
        _getPatientBedMakingSummaryQuery = getPatientBedMakingSummaryQuery;
        _deleteBedMakingCommandHandler = deleteBedMakingCommandHandler;
    }
    
    [AbpAuthorize(AppPermissions.Pages_BedMaking_Create)]
    public async Task CreateBedMaking(CreateBedMakingDto input) 
        => await _createBedMakingCommandHandler.Handle(input);
    
    public async Task<List<PatientBedMakingSummaryForReturnDto>> GetPatientSummary(int patientId) 
        => await _getPatientBedMakingSummaryQuery.Handle(patientId, AbpSession.TenantId); 

    [AbpAuthorize(AppPermissions.Pages_BedMaking_Delete)]
    public async Task DeleteCreateBedMaking(long bedMakingId) 
        => await _deleteBedMakingCommandHandler.Handle(bedMakingId, AbpSession.UserId);

}