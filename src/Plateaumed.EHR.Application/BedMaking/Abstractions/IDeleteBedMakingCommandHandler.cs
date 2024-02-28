using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.BedMaking.Abstractions;

public interface IDeleteBedMakingCommandHandler : ITransientDependency
{
    Task Handle(long bedMakingId, long? userId);
}