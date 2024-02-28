using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.InputNotes.Dtos;

namespace Plateaumed.EHR.InputNotes.Abstractions;

public interface ICreateInputNotesCommandHandler : ITransientDependency
{
    Task<AllInputs.InputNotes> Handle(CreateInputNotesDto inputNotes);
}