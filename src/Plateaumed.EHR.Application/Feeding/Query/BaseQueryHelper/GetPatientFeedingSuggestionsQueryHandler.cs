using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Feeding.Abstractions;
using Plateaumed.EHR.Feeding.Dtos;

namespace Plateaumed.EHR.Feeding.Query.BaseQueryHelper;

public class GetPatientFeedingSuggestionsQueryHandler : IGetPatientFeedingSuggestionsQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IObjectMapper _objectMapper;
    
    public GetPatientFeedingSuggestionsQueryHandler(IBaseQuery baseQuery, IObjectMapper objectMapper)
    {
        _baseQuery = baseQuery;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<FeedingSuggestionsReturnDto>> Handle(FeedingType feedingType)
    {
        var options = await _baseQuery.GetPatientFeedingSuggestionsBaseQuery()
            .Where(v => v.Type == feedingType)
            .OrderByDescending(v => v.Id)
            .ToListAsync();
        
        return  _objectMapper.Map<List<FeedingSuggestionsReturnDto>>(options);
    }
}