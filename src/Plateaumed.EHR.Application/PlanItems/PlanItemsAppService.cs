using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.PlanItems.Abstractions;
using Plateaumed.EHR.PlanItems.Dtos;

namespace Plateaumed.EHR.PlanItems;

[AbpAuthorize(AppPermissions.Pages_PlanItems)]
public class PlanItemsAppService : EHRAppServiceBase, IPlanItemsAppService
{
    private readonly ICreatePlanItemsCommandHandler _createPlanItemsCommandHandler;
    private readonly IGetPatientPlanItemsSummaryQueryHandler _getPatientPlanItemsSummaryQuery;
    private readonly IDeletePlanItemsCommandHandler _deletePlanItemsCommandHandler;
    
    public PlanItemsAppService(ICreatePlanItemsCommandHandler createPlanItemsCommandHandler, IGetPatientPlanItemsSummaryQueryHandler getPatientPlanItemsSummaryQuery, IDeletePlanItemsCommandHandler deletePlanItemsCommandHandler)
    {
        _createPlanItemsCommandHandler = createPlanItemsCommandHandler;
        _getPatientPlanItemsSummaryQuery = getPatientPlanItemsSummaryQuery;
        _deletePlanItemsCommandHandler = deletePlanItemsCommandHandler;
    }

    [AbpAuthorize(AppPermissions.Pages_PlanItems_Create)]
    public async Task CreatePlanItems(CreatePlanItemsDto input)
        => await _createPlanItemsCommandHandler.Handle(input);

    public async Task<List<PlanItemsSummaryForReturnDto>> GetPatientPlanItems(GetPlanItemsDto planItemsDto) 
        => await _getPatientPlanItemsSummaryQuery.Handle(planItemsDto.PatientId, AbpSession.TenantId, planItemsDto.ProcedureId); 

    [AbpAuthorize(AppPermissions.Pages_PlanItems_Delete)]
    public async Task DeleteCreatePlanItems(long planItemsId) 
        => await _deletePlanItemsCommandHandler.Handle(planItemsId, AbpSession.UserId);

}