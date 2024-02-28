using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Meals.Abstractions;
using Plateaumed.EHR.Meals.Dtos;

namespace Plateaumed.EHR.Meals;

[AbpAuthorize(AppPermissions.Pages_Meals)]
public class MealsAppService : EHRAppServiceBase, IMealsAppService
{
    private readonly ICreateMealsCommandHandler _createMealsCommandHandler;
    private readonly IGetPatientMealsSummaryQueryHandler _getPatientMealsSummaryQuery;
    private readonly IDeleteMealsCommandHandler _deleteMealsCommandHandler;
    
    public MealsAppService(ICreateMealsCommandHandler createMealsCommandHandler, IGetPatientMealsSummaryQueryHandler getPatientMealsSummaryQuery, IDeleteMealsCommandHandler deleteMealsCommandHandler)
    {
        _createMealsCommandHandler = createMealsCommandHandler;
        _getPatientMealsSummaryQuery = getPatientMealsSummaryQuery;
        _deleteMealsCommandHandler = deleteMealsCommandHandler;
    }
    
    [AbpAuthorize(AppPermissions.Pages_Meals_Create)]
    public async Task CreateMeals(CreateMealsDto input) 
        => await _createMealsCommandHandler.Handle(input);
    
    public async Task<List<MealsSummaryForReturnDto>> GetPatientMeals(GetMealsDto mealsDto) 
        => await _getPatientMealsSummaryQuery.Handle(mealsDto.PatientId); 

    [AbpAuthorize(AppPermissions.Pages_Meals_Delete)]
    public async Task DeleteCreateMeals(long mealsId) 
        => await _deleteMealsCommandHandler.Handle(mealsId);

}