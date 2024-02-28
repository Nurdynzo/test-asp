using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.InputNotes.Abstractions;
using Plateaumed.EHR.InputNotes.Dtos;

namespace Plateaumed.EHR.InputNotes;

[AbpAuthorize(AppPermissions.Pages_InputNotes)]
public class InputNotesAppService : EHRAppServiceBase, IInputNotesAppService
{
    private readonly ICreateInputNotesCommandHandler _createInputNotesCommandHandler;
    private readonly IGetPatientInputNotesSummaryQueryHandler _getPatientInputNotesSummaryQuery;
    private readonly IDeleteInputNotesCommandHandler _deleteInputNotesCommandHandler;
    
    public InputNotesAppService(ICreateInputNotesCommandHandler createInputNotesCommandHandler, IGetPatientInputNotesSummaryQueryHandler getPatientInputNotesSummaryQuery, IDeleteInputNotesCommandHandler deleteInputNotesCommandHandler)
    {
        _createInputNotesCommandHandler = createInputNotesCommandHandler;
        _getPatientInputNotesSummaryQuery = getPatientInputNotesSummaryQuery;
        _deleteInputNotesCommandHandler = deleteInputNotesCommandHandler;
    }
    
    [AbpAuthorize(AppPermissions.Pages_InputNotes_Create)]
    public async Task CreateInputNotes(CreateInputNotesDto input) 
        => await _createInputNotesCommandHandler.Handle(input);
    
    public async Task<List<InputNotesSummaryForReturnDto>> GetPatientInputNotes(GetInputNotesDto inputNotesDto) 
        => await _getPatientInputNotesSummaryQuery.Handle(inputNotesDto);

    [AbpAuthorize(AppPermissions.Pages_InputNotes_Delete)]
    public async Task DeleteCreateInputNotes(long inputNotesId) 
        => await _deleteInputNotesCommandHandler.Handle(inputNotesId);

}
