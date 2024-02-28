using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Abstractions;

public interface IEditIntakeOutputCommandHandler : ITransientDependency
{
    Task<IntakeOutputReturnDto> Handle(CreateIntakeOutputDto requestDto);
}
