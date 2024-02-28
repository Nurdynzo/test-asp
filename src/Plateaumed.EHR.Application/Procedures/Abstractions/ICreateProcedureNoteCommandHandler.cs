using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface ICreateProcedureNoteCommandHandler : ITransientDependency
{
    Task Handle(CreateProcedureNoteDto requestDto);
}