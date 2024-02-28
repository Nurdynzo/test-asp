using System.Linq;
using Abp.Dependency;

namespace Plateaumed.EHR.Meals.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    IQueryable<AllInputs.Meals> GetPatientMealsBaseQuery(int patientId, bool isDeleted = false);
}