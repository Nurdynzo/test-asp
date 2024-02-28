using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Symptom;

namespace Plateaumed.EHR.Snowstorm.Query.BaseQueryHelper;

public class SuggestionBaseQuery : ISuggestionBaseQuery
{
    private readonly IRepository<SnowmedBodyPart, long> _bodypartsRepository;
    private readonly IRepository<SnowmedSuggestion, long> _suggestionsRepository; 
    
    public SuggestionBaseQuery(IRepository<SnowmedBodyPart, long> bodypartsRepository, IRepository<SnowmedSuggestion, long> suggestionsRepository)
    {
        _bodypartsRepository = bodypartsRepository;
        _suggestionsRepository = suggestionsRepository;
    }
    
    public IQueryable<SnowmedBodyPart> GetDistinctBodyPartsBaseQuery()
    { 
        var query = _bodypartsRepository.GetAll().GroupBy(v => v.Synonym).Select(g => g.FirstOrDefault());
        return query;
    }
    
    public IQueryable<SnowmedBodyPart> GetBodyPartsBySynonymBaseQuery(string synonym)
    {
        var query = (from a in _bodypartsRepository.GetAll()
                    where a.Synonym == synonym
                    select a);
        
        return query;
    }

    public IQueryable<SnowmedSuggestion> GetSuggestionsBaseQuery(string sourceSnowmedId, AllInputType inputType)
    {
        IQueryable<SnowmedSuggestion> query = default;
        if (!string.IsNullOrEmpty(sourceSnowmedId) && sourceSnowmedId != "0")
        { 
            query = (from a in _suggestionsRepository.GetAll()
                where a.SourceSnowmedId == sourceSnowmedId && a.Type == inputType 
                select a);
        }
        else
        { 
            query = (from a in _suggestionsRepository.GetAll() 
                where a.Type == inputType 
                select a);
        }
        
        return query;
    }
}