using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.WoundDressing.Abstractions;
using Plateaumed.EHR.WoundDressing.Dtos;

namespace Plateaumed.EHR.WoundDressing;

[AbpAuthorize(AppPermissions.Pages_WoundDressing)]
public class WoundDressingAppService : EHRAppServiceBase, IWoundDressingAppService
{
    private readonly ICreateWoundDressingCommandHandler _createWoundDressingCommandHandler;
    private readonly IGetPatientWoundDressingSummaryQueryHandler _getPatientWoundDressingSummaryQuery;
    private readonly IDeleteWoundDressingCommandHandler _deleteWoundDressingCommandHandler;
    
    public WoundDressingAppService(ICreateWoundDressingCommandHandler createWoundDressingCommandHandler, IGetPatientWoundDressingSummaryQueryHandler getPatientWoundDressingSummaryQuery, IDeleteWoundDressingCommandHandler deleteWoundDressingCommandHandler)
    {
        _createWoundDressingCommandHandler = createWoundDressingCommandHandler;
        _getPatientWoundDressingSummaryQuery = getPatientWoundDressingSummaryQuery;
        _deleteWoundDressingCommandHandler = deleteWoundDressingCommandHandler;
    }
    
    [AbpAuthorize(AppPermissions.Pages_WoundDressing_Create)]
    public async Task CreateWoundDressing(CreateWoundDressingDto input) 
        => await _createWoundDressingCommandHandler.Handle(input);
    
    public async Task<List<WoundDressingSummaryForReturnDto>> GetPatientWoundDressing(GetWoundDressingDto woundDressingDto) 
        => await _getPatientWoundDressingSummaryQuery.Handle(woundDressingDto.PatientId); 

    [AbpAuthorize(AppPermissions.Pages_WoundDressing_Delete)]
    public async Task DeleteCreateWoundDressing(long woundDressingId) 
        => await _deleteWoundDressingCommandHandler.Handle(woundDressingId);

}