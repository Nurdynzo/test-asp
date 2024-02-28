using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PlanItems.Dtos;

namespace Plateaumed.EHR.PlanItems.Abstractions;

public interface ICreatePlanItemsCommandHandler : ITransientDependency
{
    Task<AllInputs.PlanItems> Handle(CreatePlanItemsDto planItems);
}