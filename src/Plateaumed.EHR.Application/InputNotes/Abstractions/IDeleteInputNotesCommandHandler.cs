using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.InputNotes.Abstractions;

public interface IDeleteInputNotesCommandHandler : ITransientDependency
{
    Task Handle(long inputNotesId);
}