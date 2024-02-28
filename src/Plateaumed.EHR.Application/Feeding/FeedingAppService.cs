using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Feeding.Abstractions;
using Plateaumed.EHR.Feeding.Dtos;

namespace Plateaumed.EHR.Feeding;

[AbpAuthorize(AppPermissions.Pages_Feeding)]
public class FeedingAppService : EHRAppServiceBase, IFeedingAppService
{
    private readonly ICreateFeedingCommandHandler _createFeedingCommandHandler;
    private readonly IGetPatientFeedingSummaryQueryHandler _getPatientFeedingSummaryQuery;
    private readonly IGetPatientFeedingSuggestionsQueryHandler _getPatientFeedingSuggestionsQuery;
    private readonly IDeleteFeedingCommandHandler _deleteFeedingCommandHandler;
    
    public FeedingAppService(ICreateFeedingCommandHandler createFeedingCommandHandler, IGetPatientFeedingSummaryQueryHandler getPatientFeedingSummaryQuery, IGetPatientFeedingSuggestionsQueryHandler getPatientFeedingSuggestionsQuery,IDeleteFeedingCommandHandler deleteFeedingCommandHandler)
    {
        _createFeedingCommandHandler = createFeedingCommandHandler;
        _getPatientFeedingSummaryQuery = getPatientFeedingSummaryQuery;
        _getPatientFeedingSuggestionsQuery = getPatientFeedingSuggestionsQuery;
        _deleteFeedingCommandHandler = deleteFeedingCommandHandler;
    }
    
    [AbpAuthorize(AppPermissions.Pages_Feeding_Create)]
    public async Task CreateFeeding(CreateFeedingDto input) 
        => await _createFeedingCommandHandler.Handle(input);
    
    public async Task<List<FeedingSummaryForReturnDto>> GetPatientFeeding(GetFeedingDto feedingDto) 
        => await _getPatientFeedingSummaryQuery.Handle(feedingDto.PatientId); 
    
    public async Task<List<FeedingSuggestionsReturnDto>> GetFeedingSuggestions(FeedingType feedingType) 
        => await _getPatientFeedingSuggestionsQuery.Handle(feedingType); 
    
    [AbpAuthorize(AppPermissions.Pages_Feeding_Delete)]
    public async Task DeleteCreateFeeding(long feedingId) 
        => await _deleteFeedingCommandHandler.Handle(feedingId);

}