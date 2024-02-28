using System.Collections.Generic;
using System.Linq;
using Abp.Dependency;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Symptom;

namespace Plateaumed.EHR.Snowstorm.Abstractions;

public interface ISuggestionBaseQuery : ITransientDependency
{
    IQueryable<SnowmedBodyPart> GetDistinctBodyPartsBaseQuery();
    IQueryable<SnowmedBodyPart> GetBodyPartsBySynonymBaseQuery(string synonym);
    IQueryable<SnowmedSuggestion> GetSuggestionsBaseQuery(string sourceSnowmedId, AllInputType inputType);
}