using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface IUpdateStatusCommandHandler : ITransientDependency
{
    Task Handle(UpdateProcedureStatusDto requestDto);
}