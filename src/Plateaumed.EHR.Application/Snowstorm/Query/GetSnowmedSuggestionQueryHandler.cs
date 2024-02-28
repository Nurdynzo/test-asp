using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;
using Plateaumed.EHR.Symptom;

namespace Plateaumed.EHR.Snowstorm.Query;

public class GetSnowmedSuggestionQueryHandler : IGetSnowmedSuggestionQueryHandler
{
    private readonly ISuggestionBaseQuery _suggestionBaseQuery;
    private readonly IObjectMapper _objectMapper;
    
    public GetSnowmedSuggestionQueryHandler(ISuggestionBaseQuery suggestionBaseQuery, IObjectMapper objectMapper)
    {
        _suggestionBaseQuery = suggestionBaseQuery;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<SnowstormSimpleResponseDto>> Handle(long? snowmedId, string inputType, string searchTerm)
    {
        // TODO: add functionality to enable get suggestions based on snowmedId and shoule not be groupedby
        var responseData = new List<SnowstormSimpleResponseDto>();
        
        var type = (AllInputType)Enum.Parse(typeof(AllInputType), inputType, true);
        if (type == AllInputType.Site)
        {
            var distinctResult = await _suggestionBaseQuery.GetDistinctBodyPartsBaseQuery().ToListAsync();
            
            // loop through the result
            for (int i = 0; i < distinctResult.Count; i++)
            {
                // Split the text into individual words using a comma as the delimiter
                string[] words = distinctResult[i].Synonym.Split(',');
                // loop through the words
                for (int j = 0; j < words.Length; j++)
                {
                    searchTerm = searchTerm.ToLower();
                    if (searchTerm.Contains(words[j]))
                    {
                        searchTerm = distinctResult[i].Synonym;
                        break;
                    }
                }
            }
            
            var bodyParts = await _suggestionBaseQuery.GetBodyPartsBySynonymBaseQuery(searchTerm).ToListAsync();
            responseData =  _objectMapper.Map<List<SnowstormSimpleResponseDto>>(bodyParts);  
        }
        else if (type == AllInputType.Character || 
                 type == AllInputType.Associations || 
                 type == AllInputType.Exacerbating ||
                 type == AllInputType.BedMaking || 
                 type == AllInputType.PlanItems || 
                 type == AllInputType.InputNotes || 
                 type == AllInputType.WoundDressing || 
                 type == AllInputType.Meals) // TODO: Refactor to List contains
        { 
            // get suggestions by input type
            var sourceSnowmedId = snowmedId.ToString();
            var suggestions = await _suggestionBaseQuery.GetSuggestionsBaseQuery(sourceSnowmedId, type).ToListAsync();
            responseData =  _objectMapper.Map<List<SnowstormSimpleResponseDto>>(suggestions);  
        }
        else if(type == AllInputType.Procedure)
        {
            responseData = await _suggestionBaseQuery.GetSuggestionsBaseQuery(null, type)
                .GroupBy(v => v.SourceSnowmedId)
                .Select(g => _objectMapper.Map<SnowstormSimpleResponseDto>(g.FirstOrDefault()))
                .ToListAsync();
        }
        
        // return response
        return responseData;
    }
}