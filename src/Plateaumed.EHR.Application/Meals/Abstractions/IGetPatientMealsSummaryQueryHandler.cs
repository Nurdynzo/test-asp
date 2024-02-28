using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Meals.Dtos;

namespace Plateaumed.EHR.Meals.Abstractions;

public interface IGetPatientMealsSummaryQueryHandler : ITransientDependency
{
    Task<List<MealsSummaryForReturnDto>> Handle(int patientId);
}