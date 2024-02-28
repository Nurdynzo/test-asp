using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Feeding.Dtos;

namespace Plateaumed.EHR.Feeding.Abstractions;

public interface ICreateFeedingCommandHandler : ITransientDependency
{
    Task<AllInputs.Feeding> Handle(CreateFeedingDto feeding);
}