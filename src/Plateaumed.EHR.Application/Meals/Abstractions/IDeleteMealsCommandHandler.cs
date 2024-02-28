using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.Meals.Abstractions;

public interface IDeleteMealsCommandHandler : ITransientDependency
{
    Task Handle(long mealsId);
}