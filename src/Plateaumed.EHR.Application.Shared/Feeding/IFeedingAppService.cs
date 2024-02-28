using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Feeding.Dtos;

namespace Plateaumed.EHR.Feeding;

public interface IFeedingAppService : IApplicationService
{ 
    Task CreateFeeding(CreateFeedingDto input);
    Task<List<FeedingSummaryForReturnDto>> GetPatientFeeding(GetFeedingDto feedingDto);

    Task<List<FeedingSuggestionsReturnDto>> GetFeedingSuggestions(FeedingType feedingType);

    Task DeleteCreateFeeding(long planItemId);
}