using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Meals.Abstractions;
using Plateaumed.EHR.Meals.Dtos;

namespace Plateaumed.EHR.Meals.Query.BaseQueryHelper;

public class GetPatientMealsSummaryQueryHandler : IGetPatientMealsSummaryQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IObjectMapper _objectMapper;
    
    public GetPatientMealsSummaryQueryHandler(IBaseQuery baseQuery, IObjectMapper objectMapper)
    {
        _baseQuery = baseQuery;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<MealsSummaryForReturnDto>> Handle(int patientId)
    {
        var options = await _baseQuery.GetPatientMealsBaseQuery(patientId).OrderByDescending(v => v.Id).ToListAsync();
        return  _objectMapper.Map<List<MealsSummaryForReturnDto>>(options);
    }
}