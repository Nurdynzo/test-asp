using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Meals.Dtos;

namespace Plateaumed.EHR.Meals.Abstractions;

public interface ICreateMealsCommandHandler : ITransientDependency
{
    Task<AllInputs.Meals> Handle(CreateMealsDto Meals);
}