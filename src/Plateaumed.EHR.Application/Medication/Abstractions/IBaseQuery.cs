using System.Linq;
using Abp.Dependency;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    IQueryable<SearchMedicationForReturnDto> SearchProductBaseQuery(string searchTerm);
    IQueryable<SearchMedicationForReturnDto> SearchActiveIngredientBaseQuery(string searchTerm);
    IQueryable<SearchMedicationForReturnDto> SearchCategoryBaseQuery(string searchTerm);
    IQueryable<SearchMedicationForReturnDto> SearchDoseFormBaseQuery(string searchTerm);
    IQueryable<AllInputs.Medication> GetPatientMedications(int patientId, long? procedureId = null); 
}