using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;

namespace Plateaumed.EHR.Snowstorm.Query;

public class GetDiagnosisQueryHandler : IGetDiagnosisQueryHandler
{
    private readonly ISnowstormBaseQuery _snowstormBaseQuery;
    private readonly IObjectMapper _objectMapper;
    
    public GetDiagnosisQueryHandler(ISnowstormBaseQuery snowstormBaseQuery, IObjectMapper objectMapper)
    {
        _snowstormBaseQuery = snowstormBaseQuery;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<SnowstormSimpleResponseDto>> Handle(SnowstormRequestDto request)
    {
        if (request.Term.Length <= 3)
            return new List<SnowstormSimpleResponseDto>();
        
        var response = await _snowstormBaseQuery.GetTermBySemanticTags(request);
        
        if(response.status == false)
            throw new UserFriendlyException($"An error occured.");

        return _objectMapper.Map<List<SnowstormSimpleResponseDto>>(response.snowstormResponse?.Items); 
    }
}