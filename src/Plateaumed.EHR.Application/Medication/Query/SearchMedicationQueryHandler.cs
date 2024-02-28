using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;

namespace Plateaumed.EHR.Medication.Query;

public class SearchMedicationQueryHandler : ISearchMedicationQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IObjectMapper _objectMapper;
    private readonly IGetDiagnosisQueryHandler _getDiagnosisQueryHandler;
    
    public SearchMedicationQueryHandler(IBaseQuery baseQuery, IObjectMapper objectMapper, IGetDiagnosisQueryHandler getDiagnosisQueryHandler)
    {
        _baseQuery = baseQuery;
        _objectMapper = objectMapper;
        _getDiagnosisQueryHandler = getDiagnosisQueryHandler;
    }
    
    public async Task<List<SearchMedicationForReturnDto>> Handle(string searchTerm)
    { 
        if (searchTerm.Length < 3)
            return new List<SearchMedicationForReturnDto>();

         var result = await _baseQuery.SearchProductBaseQuery(searchTerm).Take(300).ToListAsync();
         
         // if result empty, search active ingredients
         if (result.Count <= 3)
             result.AddRange(await _baseQuery.SearchActiveIngredientBaseQuery(searchTerm).Take(300).ToListAsync());

         // if result empty, search category
         if (result.Count <= 3)
             result.AddRange(await _baseQuery.SearchCategoryBaseQuery(searchTerm).Take(300).ToListAsync());
         
         // if result empty, search doseform
         if (result.Count <= 3)
             result.AddRange(await _baseQuery.SearchDoseFormBaseQuery(searchTerm).Take(300).ToListAsync());
         
         // if result empty, search snowmed
         if (result.Count <= 3)
         {
             var semanticTag = "clinical drug";
             var drugs = await _getDiagnosisQueryHandler.Handle(new SnowstormRequestDto
             {
                 Term = searchTerm,
                 SemanticTag = semanticTag
             });

             result.AddRange(_objectMapper.Map<List<SearchMedicationForReturnDto>>(drugs));
         }
         
         // return result
         return result;
    }
}