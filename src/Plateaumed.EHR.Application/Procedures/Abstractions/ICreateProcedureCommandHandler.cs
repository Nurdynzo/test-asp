using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface ICreateProcedureCommandHandler : ITransientDependency
{
    Task Handle(CreateProcedureDto requestDto);
}