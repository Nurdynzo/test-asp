using System.Linq;
using Abp.Dependency;

namespace Plateaumed.EHR.Feeding.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    IQueryable<AllInputs.Feeding> GetPatientFeedingBaseQuery(int patientId);
    
    IQueryable<AllInputs.FeedingSuggestions> GetPatientFeedingSuggestionsBaseQuery();

}