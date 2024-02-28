using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.BedMaking.Dtos;

namespace Plateaumed.EHR.BedMaking.Abstractions;

public interface ICreateBedMakingCommandHandler : ITransientDependency
{
    Task<AllInputs.BedMaking> Handle(CreateBedMakingDto bedMaking);
}