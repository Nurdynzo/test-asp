using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Feeding.Abstractions;
using Plateaumed.EHR.Feeding.Dtos;

namespace Plateaumed.EHR.Feeding.Query.BaseQueryHelper;

public class GetPatientFeedingSummaryQueryHandler : IGetPatientFeedingSummaryQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IObjectMapper _objectMapper;
    
    public GetPatientFeedingSummaryQueryHandler(IBaseQuery baseQuery, IObjectMapper objectMapper)
    {
        _baseQuery = baseQuery;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<FeedingSummaryForReturnDto>> Handle(int patientId)
    {
        var options = await _baseQuery.GetPatientFeedingBaseQuery(patientId).OrderByDescending(v => v.Id).ToListAsync();
        return _objectMapper.Map<List<FeedingSummaryForReturnDto>>(options);
    }
}