using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface IGetNoteTemplatesQueryHandler : ITransientDependency
{
    Task<List<NoteTemplateResponseDto>> Handle(string noteRequestType);
}