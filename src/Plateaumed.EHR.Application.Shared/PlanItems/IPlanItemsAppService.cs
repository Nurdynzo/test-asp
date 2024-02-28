using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.PlanItems.Dtos;

namespace Plateaumed.EHR.PlanItems;

public interface IPlanItemsAppService : IApplicationService
{ 
    Task CreatePlanItems(CreatePlanItemsDto input);
    Task<List<PlanItemsSummaryForReturnDto>> GetPatientPlanItems(GetPlanItemsDto planItemsDto);
    Task DeleteCreatePlanItems(long planItemId);
}