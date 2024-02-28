using System;
using System.Threading.Tasks;  
using Microsoft.Extensions.Configuration;
using Plateaumed.EHR.Configuration;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Snowstorm.Query.BaseQueryHelper;

public class SnowstormBaseQuery : ISnowstormBaseQuery
{
    private readonly IHttpResourceService _httpResourceService;
    public ILog _logger;
    readonly IConfigurationRoot _appConfiguration;
    
    public SnowstormBaseQuery(IHttpResourceService httpResourceService, ILog logger, IAppConfigurationAccessor appConfigurationAccessor)
    {
        _httpResourceService = httpResourceService;
        _logger = logger;
        _appConfiguration = appConfigurationAccessor.Configuration;
    }
    
    public async Task<(SnowstormResponse snowstormResponse, bool status)> GetTermBySemanticTags(SnowstormRequestDto request)
    {
        // endcode the search team
        var searchTerm = Uri.EscapeDataString(request.Term);
        
        var requestUrl = 
            $"{snowmedBaseUrl}/browser/MAIN/descriptions?limit=20&term={searchTerm}&active=true&conceptActive=true&lang=english&semanticTags={request.SemanticTag}&semanticTags={request.SemanticTag2}&groupByConcept=true"; 

        _logger.Write($"Snowstorm request => {requestUrl}");
        var result =await _httpResourceService.GetAsync<SnowstormResponse>(requestUrl);

        if (result.Item2.isSuccess)
        {
            _logger.Write("Request was made successfully");
            return (snowstormResponse: result.Item1, status: true);
        }
        else
        {
            _logger.Write($"Error getting response with message {result.Item2.Message}");
            return (snowstormResponse: null, status: false);
        }
    }
    
    
    private string snowmedBaseUrl { get { return _appConfiguration.GetValue<string>("Snowstorm:BaseUrl"); } }
}