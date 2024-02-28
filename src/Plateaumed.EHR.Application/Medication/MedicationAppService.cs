using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication;

[AbpAuthorize(AppPermissions.Pages_Medications)]
public class MedicationAppService : EHRAppServiceBase, IMedicationAppService
{
    private readonly ISearchMedicationQueryHandler _searchMedicationQueryHandler;
    private readonly IMedicationSuggestionQueryHandler _medicationSuggestionQueryHandler;
    private readonly IGetMedicationForDiscontinueQueryHandler _getMedicationForDiscontinueQueryHandler;
    private readonly IAdministerMedicationCommandHandler _administerMedicationCommandHandler;
    private readonly IGetAdministerMedicationQueryHandler _getAdministerMedicationQueryHandler;
    private readonly IMarkMedicationForDiscontinueCommandHandler _markMedicationForDiscontinueCommandHandler;
    private readonly IGetMedicationForMarkAsAdministered _getMedicationForMarkAsAdministered;
    private readonly IMarkMedicationForAdministerCommandHandler _markMedicationForAdminister;
    private readonly IDeleteMedicationCommandHandler _deleteMedicationCommandHandler;
    private readonly ICreateMedicationCommandHandler _createMedicationCommandHandler;
    private readonly IGetPatientMedicationQueryHandler _getPatientMedicationQueryHandler;
    private readonly IGetMedicationSuggestionsQueryHandler _getMedicationSuggestionsQueryHandler;
    private readonly IGetViewMedicationQueryHandler _getViewMedicationQueryHandler;


    public MedicationAppService(
        ISearchMedicationQueryHandler searchMedicationQueryHandler,
        IMedicationSuggestionQueryHandler medicationSuggestionQueryHandler,
        IAdministerMedicationCommandHandler administerMedicationCommandHandler,
        IGetAdministerMedicationQueryHandler getAdministerMedicationQueryHandler,
        IGetMedicationForDiscontinueQueryHandler getMedicationForDiscontinueQueryHandler,
        IMarkMedicationForDiscontinueCommandHandler markMedicationForDiscontinueCommandHandler,
        IGetMedicationForMarkAsAdministered getMedicationForMarkAsAdministered,
        IMarkMedicationForAdministerCommandHandler markMedicationForAdminister,
        IDeleteMedicationCommandHandler deleteMedicationCommandHandler,
        ICreateMedicationCommandHandler createMedicationCommandHandler,
        IGetPatientMedicationQueryHandler getPatientMedicationQueryHandler,
        IGetMedicationSuggestionsQueryHandler getMedicationSuggestionsQueryHandler,
        IGetViewMedicationQueryHandler getViewMedicationQueryHandler)
    {
        _searchMedicationQueryHandler = searchMedicationQueryHandler;
        _medicationSuggestionQueryHandler = medicationSuggestionQueryHandler;
        _administerMedicationCommandHandler = administerMedicationCommandHandler;
        _getAdministerMedicationQueryHandler = getAdministerMedicationQueryHandler;
        _getMedicationForDiscontinueQueryHandler = getMedicationForDiscontinueQueryHandler;
        _markMedicationForDiscontinueCommandHandler = markMedicationForDiscontinueCommandHandler;
        _markMedicationForAdminister = markMedicationForAdminister;
        _getMedicationForMarkAsAdministered = getMedicationForMarkAsAdministered;
        _deleteMedicationCommandHandler = deleteMedicationCommandHandler;
        _createMedicationCommandHandler = createMedicationCommandHandler;
        _getPatientMedicationQueryHandler = getPatientMedicationQueryHandler;
        _getMedicationSuggestionsQueryHandler = getMedicationSuggestionsQueryHandler;
        _getViewMedicationQueryHandler = getViewMedicationQueryHandler;
    }
    
    [AbpAuthorize(AppPermissions.Pages_Medications_Create)]
    public async Task CreateMedication(CreateMultipleMedicationsDto input) => await _createMedicationCommandHandler.Handle(input);
    
    public async Task<List<SearchMedicationForReturnDto>> GetSearchedMedications(string searchTerm) =>  await _searchMedicationQueryHandler.Handle(searchTerm.ToLower());
 
    public MedicationSuggestionForReturnDto GetMedicationSuggestions()
        =>  _medicationSuggestionQueryHandler.Handle();
    public async Task<List<MedicalViewResponse>> GetViewMedication(long patientId)
        => await _getViewMedicationQueryHandler.Handle(patientId);

    public async Task<List<PatientMedicationForReturnDto>> GetPatientPrescriptions(int patientId, long? procedureId = null)
        => await _getPatientMedicationQueryHandler.Handle(patientId, procedureId);
    public async Task<List<MedicationSummaryQueryResponse>> GetMedicationForDiscontinue(long patientId, long encounterId)
        => await _getMedicationForDiscontinueQueryHandler.Handle(patientId, encounterId);
    public async Task<List<MedicationSummaryQueryResponse>> GetMedicationForMarkAsAdministered(long patientId, long encounterId)
        => await _getMedicationForMarkAsAdministered.Handle(patientId, encounterId);
    public async Task MarkMedicationForDiscontinue(List<long> medicationId)
        => await _markMedicationForDiscontinueCommandHandler.Handle(medicationId);
    public async Task MarkMedicationForAdminister(List<long> medicationId)
        => await _markMedicationForAdminister.Handle(medicationId);
    public async Task DeleteMedication(long id)
        => await _deleteMedicationCommandHandler.Handle(id);

    public async Task AdministerMedication(MedicationAdministrationActivityRequest request)
        => await _administerMedicationCommandHandler.Handle(request);

    public async Task<List<MedicationAdministrationActivityResponse>> GetAdministeredMedication(long encounterId)
        => await _getAdministerMedicationQueryHandler.Handle(encounterId);

    public async Task<List<SearchMedicationForReturnDto>> GetMedicationSugguestions() =>  await _getMedicationSuggestionsQueryHandler.Handle();
}
