using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication.Abstractions;

public interface IGetMedicationSuggestionsQueryHandler : ITransientDependency
{
    Task<List<SearchMedicationForReturnDto>> Handle();
}