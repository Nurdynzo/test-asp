using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Snowstorm.Dtos;

namespace Plateaumed.EHR.Snowstorm.Abstractions;

public interface IGetSnowmedSuggestionQueryHandler : ITransientDependency
{
    Task<List<SnowstormSimpleResponseDto>> Handle(long? snowmedId, string inputType, string searchTerm);
}