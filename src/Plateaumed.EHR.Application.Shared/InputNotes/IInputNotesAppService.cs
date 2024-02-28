using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.InputNotes.Dtos;

namespace Plateaumed.EHR.InputNotes;

public interface IInputNotesAppService : IApplicationService
{ 
    Task CreateInputNotes(CreateInputNotesDto input);
    Task<List<InputNotesSummaryForReturnDto>> GetPatientInputNotes(GetInputNotesDto inputNotesDto);
    Task DeleteCreateInputNotes(long planItemId);
}