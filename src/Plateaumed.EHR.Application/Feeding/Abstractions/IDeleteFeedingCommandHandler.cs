using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.Feeding.Abstractions;

public interface IDeleteFeedingCommandHandler : ITransientDependency
{
    Task Handle(long feedingId);
}