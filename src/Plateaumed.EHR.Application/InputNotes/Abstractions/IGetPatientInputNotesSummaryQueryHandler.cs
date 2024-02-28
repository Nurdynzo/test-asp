using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.InputNotes.Dtos;

namespace Plateaumed.EHR.InputNotes.Abstractions;

public interface IGetPatientInputNotesSummaryQueryHandler : ITransientDependency
{
    Task<List<InputNotesSummaryForReturnDto>> Handle(GetInputNotesDto input);
}
