using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.PlanItems.Abstractions;

public interface IDeletePlanItemsCommandHandler : ITransientDependency
{
    Task Handle(long planItemsId, long? userId);
}