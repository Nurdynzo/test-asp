using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Meals.Dtos;

namespace Plateaumed.EHR.Meals;

public interface IMealsAppService : IApplicationService
{ 
    Task CreateMeals(CreateMealsDto input);
    Task<List<MealsSummaryForReturnDto>> GetPatientMeals(GetMealsDto mealsDto);
    Task DeleteCreateMeals(long planItemId);
}