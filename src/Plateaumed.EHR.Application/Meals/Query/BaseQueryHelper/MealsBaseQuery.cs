using System.Linq;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Plateaumed.EHR.Meals.Abstractions;

namespace Plateaumed.EHR.Meals.Query.BaseQueryHelper;

public class MealsBaseQuery : IBaseQuery
{
    private readonly IRepository<AllInputs.Meals, long> _mealsRepository;

    public MealsBaseQuery(IRepository<AllInputs.Meals, long> mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public IQueryable<AllInputs.Meals> GetPatientMealsBaseQuery(int patientId, bool isDeleted = false)
    {
        return _mealsRepository.GetAll()
            .Where(a => a.PatientId == patientId && a.IsDeleted == isDeleted);
    }
}