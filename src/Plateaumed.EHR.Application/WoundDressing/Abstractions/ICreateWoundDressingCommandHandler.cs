using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.WoundDressing.Dtos;

namespace Plateaumed.EHR.WoundDressing.Abstractions;

public interface ICreateWoundDressingCommandHandler : ITransientDependency
{
    Task<AllInputs.WoundDressing> Handle(CreateWoundDressingDto woundDressing);
}