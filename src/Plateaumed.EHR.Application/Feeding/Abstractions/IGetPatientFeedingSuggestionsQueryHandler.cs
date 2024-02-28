using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Feeding.Dtos;

namespace Plateaumed.EHR.Feeding.Abstractions;

public interface IGetPatientFeedingSuggestionsQueryHandler : ITransientDependency
{
    Task<List<FeedingSuggestionsReturnDto>> Handle(FeedingType feedingType);
}