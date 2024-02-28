using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication.Abstractions;

public interface IMedicationSuggestionQueryHandler : ITransientDependency
{
    MedicationSuggestionForReturnDto Handle();
}