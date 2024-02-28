using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.PatientProfile;

[AbpAuthorize(AppPermissions.Pages_Patient_Profiles)]
public class PatientProfileAppService : EHRAppServiceBase, IPatientProfileAppService
{
    private readonly IDeletePatientAllergyCommandHandler _deletePatientAllergyCommandHandler;
    private readonly IEditPatientAllergyCommandHandler _editPatientAllergyCommandHandler;
    private readonly IGetClinicalInvestigationQueryHandler _getClinicalInvestigationQueryHandler;
    private readonly IGetAwaitingClinicalInvestigationResultQueryHandler _getAwaitingClinicalInvestigationResultQueryHandler;
    private readonly ICreatePatientAllergyCommandHandler _createPatientAllergyCommandHandler;
    private readonly IGetTreatmentPlansQueryHandler _getTreatmentPlansQueryHandler;
    private readonly IGetPatientAllergyQueryHandler _getPatientAllergyQueryHandler;
    private readonly ISaveMenstruationAndFrequencyCommandHandler _saveMenstruationAndFrequencyCommandHandler;
    private readonly ISavePatientFamilyHistoryCommandHandler _savePatientFamilyHistoryCommandHandler;
    private readonly IGetPatientMenstrualFrequencyQueryHandler _getPatientMenstrualFrequencyQueryHandler;
    private readonly IGetPatientMenstrualFlowQueryHandler _getPatientMenstrualFlowQueryHandler;
    private readonly IDeleteMenstrualFrequencyCommandHandler _deleteMenstrualFrequencyCommandHandler;
    private readonly IDeletePatientMenstrualFlowCommandHandler _deletePatientMenstrualFlowCommandHandler;
    private readonly ICreatePatientPastMedicalHistoryHandler _createPatientPastMedicalHistoryHandler;
    private readonly IGetPatientPastMedicalHistoryQueryHandler _getPatientPastMedicalHistoryQueryHandler;
    private readonly IPostmenopausalSymptomQueryHandler _getPostmenopausalSymptomsQueryHandler;
    private readonly IGetPatientGynaecologicProcedureSuggestionQueryHandler _getPatientGynaecologicProcedureSuggestionQueryHandler;
    private readonly ICreatePhysicalExerciseCommandHandler _createPhysicalExerciseCommandHandler;
    private readonly IGetPatientPhysicalExerciseQueryHandler _getPatientPhysicalExerciseQueryHandler;
    private readonly ICreatePatientTravelHistoryCommandHandler _createPatientTravelHistoryCommandHandler;
    private readonly IGetPatientTravelHistoryQueryHandler _getPatientTravelHistoryQueryHandler;
    private readonly IDeletePatientMajorInjuryCommandHandler _deletePatientMajorInjuryCommandHandler;
    private readonly IGetChronicDiseaseSuggestionQueryHandler _getChronicDiseaseSuggestionQueryHandler;
    private readonly ISaveMenstrualBloodFlowCommandHandler _saveMenstrualBloodFlowCommandHandler;
    private readonly IGetAllergyTypeSuggestionQueryHandler _getAllergyTypeSuggestionQueryHandler;
    private readonly IGetPatientGynaecologicalIllnessSuggestionQueryHandler _getPatientGynaecologicalIllnessSuggestionQueryHandler;
    private readonly IGetAllergyExperiencedSuggestionQueryHandler _getAllergyExperiencedSuggestionQueryHandler;
    private readonly IDeletePatientChronicConditionsCommandHandler _deletePatientChronicConditionsCommandHandler;
    private readonly IUpdatePatientGenotypeAndBloodGroupCommandHandler _updatePatientGenotypeAndBloodGroupCommandHandler;
    private readonly ICreatePatientMajorInjuryCommandHandler _createPatientMajorInjuryCommandHandler;
    private readonly IGetContraceptionSuggestionQueryHandler _getContraceptionSuggestionQueryHandler;
    private readonly IGetPatientMajorInjuryQueryHandler _getPatientMajorInjuryQueryHandler;
    private readonly IGetImplantSuggestionQueryHandler _getImplantSuggestionQueryHandler;
    private readonly ICreatePatientImplantCommandHandler _createPatientImplantCommandHandler;
    private readonly IGetReviewOfSystemsSuggestionsHandler _getReviewOfSystemsSuggestionsHandler;
    private readonly IGetDiagnosisSuggessionsQueryHandler _getDiagnosisSuggessionsQueryHandler;
    private readonly IGetProcedureSuggestionRequestHandler _getProcedureSuggestionRequestHandler;
    private readonly IGetPatientImplantQueryHandler _getPatientImplantQueryHandler;
    private readonly ICreateBloodTransfusionHistoryHandler _createBloodTransfusionHistory;
    private readonly ICreateSurgicalHistoryCommandHandler _createSurgicalHistory;
    private readonly IGetPatientSurgicalHistoryQueryHandler _getPatientSurgicalHistory;
    private readonly IGetPatientBloodTransfusionHistory _getPatientBloodTransfusionHistory;
    private readonly IDeleteBloodTransfusionHistoryCommandHandler _deleteBloodTransfusionHistory;
    private readonly IDeletePatientSurgicalHistoryCommandHandler _deletePatientSurgicalHistoryCommandHandler;
    private readonly IUpdateSurgicalHistoryCommandHandler _updateSurgicalHistoryCommandHandler;
    private readonly IUpdateBloodTransfusionHistory _updateBloodTransfusionHistory;
    private readonly ICreateAlcoholHistoryCommandHandler _createAlcoholHistoryCommandHandler;
    private readonly ICreateRecreationalDrugHistory _createRecreationalDrugHistory;
    private readonly ICreateCigaretteAndTobaccoHistoryCommandHandler _createCigaretteAndTobaccoHistoryCommandHandler;
    private readonly IUpdateAlcoholHistoryCommandHandler _updateAlcoholHistoryCommandHandler;
    private readonly IUpdateCigaretteAndTobaccoHistoryCommandHandler _updateCigeretterAndTobaccoHandler;
    private readonly IUpdateRecreationalDrugHistoryCommandHandler _updateRecreationalDrugHandler;
    private readonly IGetAlcoholHistoryRequestHandler _getAlcoholHistoryRequestHandler;
    private readonly IGetRecreationalDrugsHistoryRequestHandler _getRecreationalDrugsHistoryRequestHandler;
    private readonly IGetCigaretteAndTobaccoHistoryQueryHandler _getCigaretteAndTobaccoHistoryQueryHandler;
    private readonly IGetAlcoholTypesSuggestionsRequestHandler _getAlcoholTypesSuggestionsRequestHandler;
    private readonly IGetCigaretteAndTobaccoSuggestionsRequestHandler _getCigaretteAndTobaccoSuggestionsRequestHandler;
    private readonly IGetRecreationalDrugsSuggestionQueryHandler _getRecreationalDrugsSuggestionQueryHandler;
    private readonly IDeleteAlcoholHistoryCommandHandler _deleteAlcoholHistoryCommandHandler;
    private readonly IDeleteCigaretteAndTobaccoHistory _deleteCigaretteAndTobaccoHistory;
    private readonly IDeleteRecreationalDrugHistoryCommandHandler _deleteRecreationalDrugHistoryCommandHandler;
    private readonly ICreateDrugHistoryCommandHandler _createDrugHistoryCommandHandler;
    private readonly IUpdateDrugHistoryCommandHandler _updateDrugHistoryCommandHandler;
    private readonly IGetDrugHistoryRequestHandler _getDrugHistoryRequestHandler;
    private readonly IDeleteDrugHistoryCommandHandler _deleteDrugHistoryCommandHandler;
    private readonly IGetPatientBloodGroupAndGenotypeRequestHandler _getPatientBloodGroupAndGenotypeRequestHandler;
    private readonly IUpdatePatientImplantCommandHandler _updatePatientImplantCommandHandler;
    private readonly IUpdateVaccinationHistoryCommandHandler _updateVaccinationHistoryCommandHandler;
    private readonly IDeletePatientImplantCommandHandler _deletePatientImplantCommandHandler;
    private readonly IGetPatientFamilyHistoryQueryHandler _getPatientFamilyHistoryQueryHandler;
    private readonly IDeleteVaccinationHistoryCommandHandler _deleteVaccinationHistoryCommandHandler;
    private readonly IGetVaccinationSuggestionsQueryHandler _getVaccinationSuggestionsQueryHandler;
    private readonly IDeletePatientTravelHistoryCommandHandler _deleteTravelHistoryCommandHandler;
    private readonly IDeletePatientFamilyMemberCommandHandler _deletePatientFamilyMemberCommandHandler;
    private readonly IUpdateFamilyMemberDetailsCommandHandler _updateFamilyMemberDetailsCommandHandler;
    private readonly ICreateReviewOfSystemsDataRequestHandler _createReviewOfSystemsRequestHandler;
    private readonly IGetPatientReviewOfSystemsDataQueryHandler _getPatientReviewOfSystemsDataQueryHandler;
    private readonly IUpdateReviewOfSystemsDataCommandHandler _updateReviewOfSystemsDataCommandHandler;
    private readonly IDeleteReviewOfSystemDataCommandHandler _deleteReviewOfSystemsDataCommandHandler;
    private readonly ICreateOccupationalHistoryCommandHandler _createOccupationalHistoryCommandHandler;
    private readonly IGetPatientOccupationalHistoryRequestHandler _getPatientOccupationalHistoryRequestHandler;
    private readonly IDeletePatientOccupationHistoryCommandHandler _deletePatientOccupationHistoryCommandHandler;
    private readonly IUpdateOccupationalHistoryCommandHandler _updateOccupationalHistoryCommandHandler;
    private readonly IGetReviewDetailsHistoryStateRequestHandler _getReviewDetailsHistoryStateRequestHandler;
    private readonly IUpdateReviewDetailsHistoryStateCommandHandler _updateReviewDetailsHistoryStateCommandHandler;
    private readonly IUpdatePatientFamilyHistoryCommandHandler _updatePatientFamilyHistoryCommandHandler;
    private readonly IDeletePastMedicalHistoryCommandHandler _deletePastMedicalHistoryCommandHandler;
    private readonly IUpdatePastMedicalConditionCommandHandler _updatePastMedicalHistoryCommandHandler;
    private readonly IUpdateMajorInjuryCommandHandler _updateMajorInjuryCommandHandler;
    public PatientProfileAppService(
        IGetClinicalInvestigationQueryHandler getClinicalInvestigationQueryHandler,
        IDeletePatientAllergyCommandHandler deletePatientAllergyCommandHandler,
        IEditPatientAllergyCommandHandler editPatientAllergyCommandHandler,
        ICreatePatientAllergyCommandHandler createPatientAllergyCommandHandler,
        IGetAwaitingClinicalInvestigationResultQueryHandler getAwaitingClinicalInvestigationResultQueryHandler,
        IGetTreatmentPlansQueryHandler getTreatmentPlansQueryHandler,
        IGetPatientAllergyQueryHandler getPatientAllergyQueryHandler, 
        ICreatePatientPastMedicalHistoryHandler createPatientPastMedicalHistoryHandler,
        ISavePatientFamilyHistoryCommandHandler savePatientFamilyHistoryCommandHandler,
        ISaveMenstruationAndFrequencyCommandHandler saveMenstruationAndFrequencyCommandHandler,
        IGetPatientMenstrualFrequencyQueryHandler getPatientMenstrualFrequencyQueryHandler,
        IGetPatientMenstrualFlowQueryHandler getPatientMenstrualFlowQueryHandler,
        IDeleteMenstrualFrequencyCommandHandler deleteMenstrualFrequencyCommandHandler,
        IDeletePatientMenstrualFlowCommandHandler deletePatientMenstrualFlowCommandHandler,
        IGetPatientPastMedicalHistoryQueryHandler getPatientPastMedicalHistoryQueryHandler,
        IPostmenopausalSymptomQueryHandler getPostmenopausalSymptomsQueryHandler,
        IGetPatientGynaecologicProcedureSuggestionQueryHandler getPatientGynaecologicProcedureSuggestionQueryHandler,
        ICreatePhysicalExerciseCommandHandler createPhysicalExerciseCommandHandler, 
        IDeletePatientMajorInjuryCommandHandler deletePatientMajorInjuryCommandHandler,
        IGetPatientPhysicalExerciseQueryHandler getPatientPhysicalExerciseQueryHandler, 
        ICreatePatientTravelHistoryCommandHandler createPatientTravelHistoryCommandHandler,
        IGetPatientTravelHistoryQueryHandler getPatientTravelHistoryQueryHandler, 
        IGetChronicDiseaseSuggestionQueryHandler getChronicDiseaseSuggestionQueryHandler,
        IGetAllergyExperiencedSuggestionQueryHandler getAllergyExperiencedSuggestionQueryHandler,
        IGetAllergyTypeSuggestionQueryHandler getAllergyTypeSuggestionQueryHandler,
        IGetPatientGynaecologicalIllnessSuggestionQueryHandler getPatientGynaecologicalIllnessSuggestionQueryHandler,
        ISaveMenstrualBloodFlowCommandHandler saveMenstrualBloodFlowCommandHandler,
        IDeletePatientChronicConditionsCommandHandler deletePatientChronicConditionsCommandHandler, 
        IUpdatePatientGenotypeAndBloodGroupCommandHandler updatePatientGenotypeAndBloodGroupCommandHandler,
        ICreatePatientMajorInjuryCommandHandler createPatientMajorInjuryCommandHandler,
        IGetContraceptionSuggestionQueryHandler getContraceptionSuggestionQueryHandler,
        IGetPatientMajorInjuryQueryHandler getPatientMajorInjuryQueryHandler,
        IGetImplantSuggestionQueryHandler getImplantSuggestionQueryHandler,
        ICreatePatientImplantCommandHandler createPatientImplantCommandHandler,
        IGetReviewOfSystemsSuggestionsHandler getReviewOfSystemsSuggestionsHandler,
        IGetDiagnosisSuggessionsQueryHandler getDiagnosisSuggessionsQueryHandler,
        IGetPatientImplantQueryHandler getPatientImplantQueryHandler,
        IGetProcedureSuggestionRequestHandler getProcedureSuggestionRequestHandler,
        ICreateBloodTransfusionHistoryHandler createBloodTransfusionHistory,
        ICreateSurgicalHistoryCommandHandler createSurgicalHistory,
        IGetPatientSurgicalHistoryQueryHandler getPatientSurgicalHistory,
        IGetPatientBloodTransfusionHistory getPatientBloodTransfusionHistory,
        IDeleteBloodTransfusionHistoryCommandHandler deleteBloodTransfusionHistory,
        IDeletePatientSurgicalHistoryCommandHandler deletePatientSurgicalHistoryCommandHandler,
        IUpdateSurgicalHistoryCommandHandler updateSurgicalHistoryCommandHandler,
        IUpdateBloodTransfusionHistory updateBloodTransfusionHistory,
        ICreateAlcoholHistoryCommandHandler createAlcoholHistoryCommandHandler,
        ICreateRecreationalDrugHistory createRecreationalDrugHistory,
        ICreateCigaretteAndTobaccoHistoryCommandHandler createCigaretteAndTobaccoHistoryCommandHandler,
        IUpdateAlcoholHistoryCommandHandler updateAlcoholHistoryCommandHandler,
        IUpdateCigaretteAndTobaccoHistoryCommandHandler updateCigeretterAndTobaccoHandler,
        IUpdateRecreationalDrugHistoryCommandHandler updateRecreationalDrugHandler,
        IGetAlcoholHistoryRequestHandler getAlcoholHistoryRequestHandler,
        IGetRecreationalDrugsHistoryRequestHandler getRecreationalDrugsHistoryRequestHandler,
        IGetCigaretteAndTobaccoHistoryQueryHandler getCigaretteAndTobaccoHistoryQueryHandler,
        IGetAlcoholTypesSuggestionsRequestHandler getAlcoholTypesSuggestionsRequestHandler,
        IGetCigaretteAndTobaccoSuggestionsRequestHandler getCigaretteAndTobaccoSuggestionsRequestHandler,
        IGetRecreationalDrugsSuggestionQueryHandler getRecreationalDrugsSuggestionQueryHandler,
        IDeleteAlcoholHistoryCommandHandler deleteAlcoholHistoryCommandHandler,
        IDeleteCigaretteAndTobaccoHistory deleteCigaretteAndTobaccoHistory,
        IDeleteRecreationalDrugHistoryCommandHandler deleteRecreationalDrugHistoryCommandHandler,
        ICreateDrugHistoryCommandHandler createDrugHistoryCommandHandler,
        IUpdateDrugHistoryCommandHandler updateDrugHistoryCommandHandler,
        IGetDrugHistoryRequestHandler getDrugHistoryRequestHandler,
        IDeleteDrugHistoryCommandHandler deleteDrugHistoryCommandHandler,
        IGetPatientBloodGroupAndGenotypeRequestHandler getPatientBloodGroupAndGenotypeRequestHandler,
        IUpdatePatientImplantCommandHandler updatePatientImplantCommandHandler,
        IUpdateVaccinationHistoryCommandHandler updateVaccinationHistoryCommandHandler,
        IDeletePatientImplantCommandHandler deletePatientImplantCommandHandler,
        IGetPatientFamilyHistoryQueryHandler getPatientFamilyHistoryQueryHandler,
        IDeleteVaccinationHistoryCommandHandler deleteVaccinationHistoryCommandHandler,
        IGetVaccinationSuggestionsQueryHandler getVaccinationSuggestionsQueryHandler,
        IDeletePatientTravelHistoryCommandHandler deleteTravelHistoryCommandHandler,
        ICreateReviewOfSystemsDataRequestHandler createReviewOfSystemsRequestHandler,
        IGetPatientReviewOfSystemsDataQueryHandler getPatientReviewOfSystemsDataQueryHandler,
        IUpdateReviewOfSystemsDataCommandHandler updateReviewOfSystemsDataCommandHandler,
        IDeleteReviewOfSystemDataCommandHandler deleteReviewOfSystemsDataCommandHandler,
        ICreateOccupationalHistoryCommandHandler createOccupationalHistoryCommandHandler,
        IGetPatientOccupationalHistoryRequestHandler getPatientOccupationalHistoryRequestHandler,
        IDeletePatientOccupationHistoryCommandHandler deletePatientOccupationHistoryCommandHandler,
        IUpdateOccupationalHistoryCommandHandler updateOccupationalHistoryCommandHandler,
        IGetReviewDetailsHistoryStateRequestHandler getReviewDetailsHistoryStateRequestHandler,
        IUpdateReviewDetailsHistoryStateCommandHandler updateReviewDetailsHistoryStateCommandHandler,
        IDeletePatientFamilyMemberCommandHandler deletePatientFamilyMemberCommandHandler,
        IUpdateFamilyMemberDetailsCommandHandler updateFamilyMemberDetailsCommandHandler,
        IUpdatePatientFamilyHistoryCommandHandler updatePatientFamilyHistoryCommandHandler,
        IDeletePastMedicalHistoryCommandHandler deletePastMedicalHistoryCommandHandler,
        IUpdatePastMedicalConditionCommandHandler updatePastMedicalHistoryCommandHandler,
        IUpdateMajorInjuryCommandHandler updateMajorInjuryCommandHandler)
    {
        _getClinicalInvestigationQueryHandler = getClinicalInvestigationQueryHandler;
        _deletePatientAllergyCommandHandler = deletePatientAllergyCommandHandler;
        _editPatientAllergyCommandHandler = editPatientAllergyCommandHandler;
        _createPatientAllergyCommandHandler = createPatientAllergyCommandHandler;
        _getAwaitingClinicalInvestigationResultQueryHandler = getAwaitingClinicalInvestigationResultQueryHandler;
        _getTreatmentPlansQueryHandler = getTreatmentPlansQueryHandler;
        _getPatientAllergyQueryHandler = getPatientAllergyQueryHandler;
        _createPatientPastMedicalHistoryHandler = createPatientPastMedicalHistoryHandler;
        _savePatientFamilyHistoryCommandHandler = savePatientFamilyHistoryCommandHandler;
        _saveMenstruationAndFrequencyCommandHandler = saveMenstruationAndFrequencyCommandHandler;
        _getPatientMenstrualFrequencyQueryHandler = getPatientMenstrualFrequencyQueryHandler;
        _getPatientMenstrualFlowQueryHandler = getPatientMenstrualFlowQueryHandler;
        _deleteMenstrualFrequencyCommandHandler = deleteMenstrualFrequencyCommandHandler;
        _deletePatientMenstrualFlowCommandHandler = deletePatientMenstrualFlowCommandHandler;
        _getPatientPastMedicalHistoryQueryHandler = getPatientPastMedicalHistoryQueryHandler;
        _getPostmenopausalSymptomsQueryHandler = getPostmenopausalSymptomsQueryHandler;
        _getPatientGynaecologicProcedureSuggestionQueryHandler = getPatientGynaecologicProcedureSuggestionQueryHandler;
        _createPhysicalExerciseCommandHandler = createPhysicalExerciseCommandHandler;
        _getPatientPhysicalExerciseQueryHandler = getPatientPhysicalExerciseQueryHandler;
        _deletePatientMajorInjuryCommandHandler = deletePatientMajorInjuryCommandHandler;
        _createPatientTravelHistoryCommandHandler = createPatientTravelHistoryCommandHandler;
        _getPatientTravelHistoryQueryHandler = getPatientTravelHistoryQueryHandler;
        _getChronicDiseaseSuggestionQueryHandler = getChronicDiseaseSuggestionQueryHandler;
        _getAllergyTypeSuggestionQueryHandler = getAllergyTypeSuggestionQueryHandler;
        _getAllergyExperiencedSuggestionQueryHandler = getAllergyExperiencedSuggestionQueryHandler;
        _getPatientGynaecologicalIllnessSuggestionQueryHandler = getPatientGynaecologicalIllnessSuggestionQueryHandler;
        _saveMenstrualBloodFlowCommandHandler = saveMenstrualBloodFlowCommandHandler;
        _deletePatientChronicConditionsCommandHandler = deletePatientChronicConditionsCommandHandler;
        _updatePatientGenotypeAndBloodGroupCommandHandler = updatePatientGenotypeAndBloodGroupCommandHandler;
        _createPatientMajorInjuryCommandHandler = createPatientMajorInjuryCommandHandler;
        _getContraceptionSuggestionQueryHandler = getContraceptionSuggestionQueryHandler;
        _getPatientMajorInjuryQueryHandler = getPatientMajorInjuryQueryHandler;
        _getImplantSuggestionQueryHandler = getImplantSuggestionQueryHandler;
        _createPatientImplantCommandHandler = createPatientImplantCommandHandler;
        _getReviewOfSystemsSuggestionsHandler = getReviewOfSystemsSuggestionsHandler;
        _getDiagnosisSuggessionsQueryHandler = getDiagnosisSuggessionsQueryHandler;
        _getProcedureSuggestionRequestHandler = getProcedureSuggestionRequestHandler;
        _createBloodTransfusionHistory = createBloodTransfusionHistory;
        _createSurgicalHistory = createSurgicalHistory;
        _getPatientSurgicalHistory = getPatientSurgicalHistory;
        _getPatientBloodTransfusionHistory = getPatientBloodTransfusionHistory;
        _deleteBloodTransfusionHistory = deleteBloodTransfusionHistory;
        _deletePatientSurgicalHistoryCommandHandler = deletePatientSurgicalHistoryCommandHandler;
        _updateSurgicalHistoryCommandHandler = updateSurgicalHistoryCommandHandler;
        _updateBloodTransfusionHistory = updateBloodTransfusionHistory;
        _createAlcoholHistoryCommandHandler = createAlcoholHistoryCommandHandler;
        _createRecreationalDrugHistory = createRecreationalDrugHistory;
        _createCigaretteAndTobaccoHistoryCommandHandler = createCigaretteAndTobaccoHistoryCommandHandler;
        _updateAlcoholHistoryCommandHandler = updateAlcoholHistoryCommandHandler;
        _updateCigeretterAndTobaccoHandler = updateCigeretterAndTobaccoHandler;
        _updateRecreationalDrugHandler = updateRecreationalDrugHandler;
        _getAlcoholHistoryRequestHandler = getAlcoholHistoryRequestHandler;
        _getRecreationalDrugsHistoryRequestHandler = getRecreationalDrugsHistoryRequestHandler;
        _getCigaretteAndTobaccoHistoryQueryHandler = getCigaretteAndTobaccoHistoryQueryHandler;
        _getAlcoholTypesSuggestionsRequestHandler = getAlcoholTypesSuggestionsRequestHandler;
        _getCigaretteAndTobaccoSuggestionsRequestHandler = getCigaretteAndTobaccoSuggestionsRequestHandler;
        _getRecreationalDrugsSuggestionQueryHandler = getRecreationalDrugsSuggestionQueryHandler;
        _deleteAlcoholHistoryCommandHandler = deleteAlcoholHistoryCommandHandler;
        _deleteCigaretteAndTobaccoHistory = deleteCigaretteAndTobaccoHistory;
        _deleteRecreationalDrugHistoryCommandHandler = deleteRecreationalDrugHistoryCommandHandler;
        _createDrugHistoryCommandHandler = createDrugHistoryCommandHandler;
        _updateDrugHistoryCommandHandler = updateDrugHistoryCommandHandler;
        _getDrugHistoryRequestHandler = getDrugHistoryRequestHandler;
        _deleteDrugHistoryCommandHandler = deleteDrugHistoryCommandHandler;
        _getPatientBloodGroupAndGenotypeRequestHandler = getPatientBloodGroupAndGenotypeRequestHandler;
        _updatePatientImplantCommandHandler = updatePatientImplantCommandHandler;
        _updateVaccinationHistoryCommandHandler = updateVaccinationHistoryCommandHandler;
        _deletePatientImplantCommandHandler = deletePatientImplantCommandHandler;
        _getPatientFamilyHistoryQueryHandler = getPatientFamilyHistoryQueryHandler;
        _deleteVaccinationHistoryCommandHandler = deleteVaccinationHistoryCommandHandler;
        _getVaccinationSuggestionsQueryHandler = getVaccinationSuggestionsQueryHandler;
        _deleteTravelHistoryCommandHandler = deleteTravelHistoryCommandHandler;
        _createReviewOfSystemsRequestHandler = createReviewOfSystemsRequestHandler;
        _getPatientReviewOfSystemsDataQueryHandler = getPatientReviewOfSystemsDataQueryHandler;
        _updateReviewOfSystemsDataCommandHandler = updateReviewOfSystemsDataCommandHandler;
        _deleteReviewOfSystemsDataCommandHandler = deleteReviewOfSystemsDataCommandHandler;
        _createOccupationalHistoryCommandHandler = createOccupationalHistoryCommandHandler;
        _getPatientOccupationalHistoryRequestHandler = getPatientOccupationalHistoryRequestHandler;
        _deletePatientOccupationHistoryCommandHandler = deletePatientOccupationHistoryCommandHandler;
        _getPatientImplantQueryHandler = getPatientImplantQueryHandler;
        _updateOccupationalHistoryCommandHandler = updateOccupationalHistoryCommandHandler;
        _getReviewDetailsHistoryStateRequestHandler = getReviewDetailsHistoryStateRequestHandler;
        _updateReviewDetailsHistoryStateCommandHandler = updateReviewDetailsHistoryStateCommandHandler;
        _deletePatientFamilyMemberCommandHandler = deletePatientFamilyMemberCommandHandler;
        _updateFamilyMemberDetailsCommandHandler = updateFamilyMemberDetailsCommandHandler;
        _updatePatientFamilyHistoryCommandHandler = updatePatientFamilyHistoryCommandHandler;
        _deletePastMedicalHistoryCommandHandler = deletePastMedicalHistoryCommandHandler;
        _updatePastMedicalHistoryCommandHandler = updatePastMedicalHistoryCommandHandler;
        _updateMajorInjuryCommandHandler = updateMajorInjuryCommandHandler;
    }

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Allergy_History_Delete)]
    public async Task DeletePatientAllergy(long id)
        => await _deletePatientAllergyCommandHandler.Handle(id);
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Allergy_History_Edit)]
    public async Task EditPatientAllergy(EditPatientAllergyCommandRequest request)
        => await _editPatientAllergyCommandHandler.Handle(request);
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Clinical_Investigation_View)]
    public async Task<List<ClinicalInvestigationResultResponse>> GetClinicalInvestigation(GetClinicalInvestigationQuery request)
        => await _getClinicalInvestigationQueryHandler.Handle(request);
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Clinical_Investigation_View)]
    public async Task<List<AwaitClinicalInvestigationResultResponse>> GetAwaitingClinicalInvestigationResult(GetClinicalInvestigationQuery request)
        => await _getAwaitingClinicalInvestigationResultQueryHandler.Handle(request);
    public async Task<List<GetPatientMenstrualFlowResponse>> GetPatientMenstrualFlow(long patientId)
        => await _getPatientMenstrualFlowQueryHandler.Handle(patientId);
    public async Task<List<GetPatientMenstrualFrequencyResponse>> GetPatientMenstrualFrequency(long patientId)
        => await _getPatientMenstrualFrequencyQueryHandler.Handle(patientId);
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Treatement_Plan_View)]
    public async Task<List<GetTreatmentPlansQueryResponse>> GetTreatmentPlans(GetTreatmentPlansRequest request)
        => await _getTreatmentPlansQueryHandler.Handle(request);
    public async Task DeleteMenstrualFrequency(long id)
        => await _deleteMenstrualFrequencyCommandHandler.Handle(id);
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_FamilyHistory_Create)]
    public async Task<PatientFamilyHistoryDto> SavePatientFamilyHistory(PatientFamilyHistoryDto request)
        => await _savePatientFamilyHistoryCommandHandler.Handle(request);
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_FamilyHistory_View)]
    public async Task<PatientFamilyHistoryDto> GetPatientFamilyHistory(long patientId)
        => await _getPatientFamilyHistoryQueryHandler.Handle(patientId);

    public async Task DeletePatientFamilyMember(long id)
        => await _deletePatientFamilyMemberCommandHandler.Handle(id);

    public async Task UpdatePatienFamilyMemberDetails(UpdateFamilyMembersDto request)
        => await _updateFamilyMemberDetailsCommandHandler.Handle(request);
    public async Task UpdatePatientFamilyHistory(PatientFamilyHistoryDto request)
        => await _updatePatientFamilyHistoryCommandHandler.Handle(request);

    public async Task DeleteMenstrualFlow(long id)
        => await _deletePatientMenstrualFlowCommandHandler.Handle(id);
    public async Task<SaveMenstruationAndFrequencyCommand> SaveMenstruationAndFrequency(SaveMenstruationAndFrequencyCommand request) =>
        await _saveMenstruationAndFrequencyCommandHandler.Handle(request);
    
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Allergy_History_Create)]
    public async Task CreatePatientAllergy(CreatePatientAllergyCommandRequest request)
    => await _createPatientAllergyCommandHandler.Handle(request);
    
    public async Task<List<PatientGynaecologicProcedureSuggestionResponse>> GetPatientGynaecologicProcedureSuggestion()
    => await _getPatientGynaecologicProcedureSuggestionQueryHandler.Handle();
    
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_FamilyHistory_View)]
    public async Task<List<GetPatientAllergyResponseDto>> GetPatientAllergies(long patientId)
        =>  await _getPatientAllergyQueryHandler.Handle(patientId);
    
    public async Task<SaveMenstrualBloodFlowCommandRequest> SaveMenstrualBloodFlow(SaveMenstrualBloodFlowCommandRequest request) 
        => await _saveMenstrualBloodFlowCommandHandler.Handle(request);
    
    public async Task<List<GetPatientGynaecologicalIllnessSuggestionQueryResponse>> GetPatientGynaecologicalIllnessSuggestion() =>
        await _getPatientGynaecologicalIllnessSuggestionQueryHandler.Handle();
    
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Chronic_Condition_Create)]
    public async Task SavePatientPastMedicalHistory(PatientPastMedicalConditionCommandRequest request) =>
        await _createPatientPastMedicalHistoryHandler.Handle(request);
    
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Chronic_Condition_View)]
    public async Task<GetPatientPastMedicalConditionQueryResponse> GetPatientPastMedicalHistory(long patientId) =>
        await _getPatientPastMedicalHistoryQueryHandler.Handle(patientId);
    
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Physical_Exercise_Create)]
    public async Task CreatePatientPhysicalExercise(CreatePhysicalExerciseCommandRequest request) =>
        await _createPhysicalExerciseCommandHandler.Handle(request);
       
    
   [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Physical_Exercise_View)]
    public async Task<GetPatientPhysicalExerciseQueryResponse> GetPatientPhysicalExercise(long patientId)
        => await _getPatientPhysicalExerciseQueryHandler.Handle(patientId);
    
   [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Travel_History_Create)]
   public async Task SavePatientTravelHistory(List<CreatePatientTravelHistoryCommand> request) =>
        await _createPatientTravelHistoryCommandHandler.Handle(request);
    
   [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Travel_History_View)]
   public async Task<GetPatientTravelHistoryQueryResponse> GetPatientTravelHistory(long patientId) =>
        await _getPatientTravelHistoryQueryHandler.Handle(patientId);

   [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Travel_History_Delete)]
   public async Task DeletePatientTravelHistory(long id) =>
        await _deleteTravelHistoryCommandHandler.Handle(id);

    public async Task<List<GetChronicDiseaseSuggestionQueryResponse>> GetChronicDiseaseSuggestion() =>
        await _getChronicDiseaseSuggestionQueryHandler.Handle();
    
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Chronic_Condition_Delete)]
    public async Task DeletePatientChronicConditions(long id) =>
        await _deletePatientChronicConditionsCommandHandler.Handle(id);
    
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_BloodGroup_Create)]
    public async Task SavePatientGenotypeAndBloodGroup(UpdatePatientGenotypeAndBloodGroupCommandRequest request)
        => await _updatePatientGenotypeAndBloodGroupCommandHandler.Handle(request);
    
    public async Task<List<GetPatientMajorInjuryResponse>> GetPatientMajorInjury(long patientId) =>
        await _getPatientMajorInjuryQueryHandler.Handle(patientId);
    public async Task SavePatientMajorInjury(CreatePatientMajorInjuryRequest request) =>
        await _createPatientMajorInjuryCommandHandler.Handle(request);
    public async Task DeletePatientMajorInjury(long id) 
        => await _deletePatientMajorInjuryCommandHandler.Handle(id);
    
    public async Task<List<PostmenopausalSymptomSuggestionResponse>> GetPostmenopausalSymptoms()
        => await _getPostmenopausalSymptomsQueryHandler.Handle();
    
    public async Task<List<GetAllergyTypeSuggestionQueryResponse>> GetAllergyTypeSuggestion() =>
        await _getAllergyTypeSuggestionQueryHandler.Handle();
    
    public async Task<List<GetContraceptionSuggestionQueryResponse>> GetContraceptionSuggestion() =>
        await _getContraceptionSuggestionQueryHandler.Handle();
    
    public async Task<List<GetAllergyExperiencedSuggestionQueryResponse>> GetAllergyExperiencedSuggestion() =>
        await _getAllergyExperiencedSuggestionQueryHandler.Handle();

    public async Task<List<GetImplantSuggestionResponse>> GetImplantSuggestions() =>
        await _getImplantSuggestionQueryHandler.Handle();

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Implant_Edit)]
    public async Task UpdatePatientImplant(CreatePatientImplantCommandRequestDto request) =>
        await _updatePatientImplantCommandHandler.Handle(request);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Implant_Delete)]
    public async Task DeletePatientImplant(long id) =>
        await _deletePatientImplantCommandHandler.Handle(id);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Implant_Create)]
    public async Task CreatePatientImplant(CreatePatientImplantCommandRequestDto request) =>
        await _createPatientImplantCommandHandler.Handle(request);

    public async Task<List<ReviewOfSystemsSuggestionResponseDto>> GetReviewOfSystemsSuggestions(SymptomsCategory category) =>
        await _getReviewOfSystemsSuggestionsHandler.Handle(category);

    public async Task<List<GetDiagnosisSuggestionResponseDto>> GetDiagnosisSuggestions() =>
        await _getDiagnosisSuggessionsQueryHandler.Handle();

    public async Task<List<GetProcedureSuggestionResponseDto>> GetProcedureSuggestions() =>
        await _getProcedureSuggestionRequestHandler.Handle();

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Implant_View)]
    public async Task<List<GetPatientImplantResponseDto>> GetPatientImplants(long patientId)=>
        await _getPatientImplantQueryHandler.Handle(patientId);

    public async Task AddBloodTransfusionHistory(CreateBloodTransfusionHistoryRequestDto request) =>
        await _createBloodTransfusionHistory.Handle(request);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Surgical_History_Create)]
    public async Task AddSurgicalHistory(CreateSurgicalHistoryRequestDto request) =>
        await _createSurgicalHistory.Handle(request);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Surgical_History_View)]
    public async Task<List<GetSurgicalHistoryResponseDto>> GetPatientSurgicalHistory(long patientId) =>
        await _getPatientSurgicalHistory.Handle(patientId);

    public async Task<List<GetPatientBloodTransfusionHistoryResponseDto>> GetPatientBloodTransfusionHistory(long patientId) =>
        await _getPatientBloodTransfusionHistory.Handle(patientId);

    public async Task DeleteBloodTransfusionHistory(long id) =>
        await _deleteBloodTransfusionHistory.Handle(id);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Surgical_History_Delete)]
    public async Task DeleteSurgicalHistory(long id) =>
        await _deletePatientSurgicalHistoryCommandHandler.Handle(id);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Surgical_History_Edit)]
    public async Task UpdateSurgicalHistory(long id, CreateSurgicalHistoryRequestDto request) =>
        await _updateSurgicalHistoryCommandHandler.Handle(id, request);

    public async Task UpdateBloodTransfusionHistory(long id, CreateBloodTransfusionHistoryRequestDto request) =>
        await _updateBloodTransfusionHistory.Handle(id, request);

    public async Task CreateAlcoholHistory(CreateAlcoholHistoryRequestDto request) =>
        await _createAlcoholHistoryCommandHandler.Handle(request);

    public async Task CreateRecreationalDrugHistory(CreateRecreationalDrugsHistoryRequestDto request) =>
        await _createRecreationalDrugHistory.Handle(request);

    public async Task CreateCigeretteAndTobaccoHistory(CreateCigaretteHistoryRequestDto request) =>
        await _createCigaretteAndTobaccoHistoryCommandHandler.Handle(request);

    public async Task UpdateAlcoholHistory(CreateAlcoholHistoryRequestDto request) =>
        await _updateAlcoholHistoryCommandHandler.Handle(request);

    public async Task UpdateCigaretteHistory(CreateCigaretteHistoryRequestDto request) =>
        await _updateCigeretterAndTobaccoHandler.Handle(request);

    public async Task UpdateRecreationalDrugHistory(CreateRecreationalDrugsHistoryRequestDto request) =>
        await _updateRecreationalDrugHandler.Handle(request);

    public async Task<List<GetAlcoholHistoryResponseDto>> GetAlcoholHistory(long patientId) =>
        await _getAlcoholHistoryRequestHandler.Handle(patientId);

    public async Task<List<GetRecreationalDrugHistoryResponseDto>> GetRecreationalDrugHistory(long patientId) =>
        await _getRecreationalDrugsHistoryRequestHandler.Handle(patientId);

    public async Task<List<GetCigaretteHistoryResponseDto>> GetCigaretteAndTobaccoHistory(long patientId) =>
        await _getCigaretteAndTobaccoHistoryQueryHandler.Handle(patientId);

    public async Task DeleteAlcoholHistory(long historyId) =>
        await _deleteAlcoholHistoryCommandHandler.Handle(historyId);

    public async Task DeleteCigaretteAndTobaccoHistory(long hisoryId) =>
        await _deleteCigaretteAndTobaccoHistory.Handle(hisoryId);

    public async Task DeleteRecreationalDrugHistory(long historyId) =>
        await _deleteRecreationalDrugHistoryCommandHandler.Handle(historyId);

    public async Task<List<GetAlcoholTypesResponseDto>> GetAlcoholTypesSuggestion() =>
        await _getAlcoholTypesSuggestionsRequestHandler.Handle();

    public async Task<List<GetTobaccoSuggestionResponseDto>> GetCigeretteAndTobaccoSuggestion() =>
        await _getCigaretteAndTobaccoSuggestionsRequestHandler.Handle();

    public async Task<List<GetRecreationalDrugsSuggesionResponseDto>> GetRecreationalDrugSuggestions() =>
        await _getRecreationalDrugsSuggestionQueryHandler.Handle();

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Drug_History_Create)]
    public async Task CreateDrugHistory(CreateDrugHistoryRequestDto request)
        =>await _createDrugHistoryCommandHandler.Handle(request);
        
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Drug_History_Edit)]
    public async Task UpdateDrugHistory(CreateDrugHistoryRequestDto request) =>
        await _updateDrugHistoryCommandHandler.Handle(request);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Drug_History_View)]
    public async Task<List<GetDrugHistoryResponseDto>> GetDrugHistory(long patientId) =>
        await _getDrugHistoryRequestHandler.Handle(patientId);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Drug_History_Delete)]
    public async Task DeleteDrugHistory(long id) =>
        await _deleteDrugHistoryCommandHandler.Handle(id);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_BloodGroup_View)]
    public async Task<GetPatientBloodGroupAndGenotypeResponseDto> GetPatientBloodGroupAndGenotype(long patientId) =>
        await _getPatientBloodGroupAndGenotypeRequestHandler.Handle(patientId);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Vaccination_History_Create)]
    public async Task UpdateVaccinationHistory(CreateVaccinationHistoryDto request) =>
        await _updateVaccinationHistoryCommandHandler.Handle(request);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Vaccination_History_Delete)]
    public async Task DeleteVaccinationHistory(long id) =>
        await _deleteVaccinationHistoryCommandHandler.Handle(id);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Vaccination_History_View)]
    public async Task<List<VaccinationSuggestionResponseDto>> GetVaccinationSuggestions() =>
        await _getVaccinationSuggestionsQueryHandler.Handle();

   [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Review_of_System_Create)]
    public async Task CreateReviewOfSystemsData(ReviewOfSystemsInputDto input) =>
        await _createReviewOfSystemsRequestHandler.Handle(input);

   [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Review_of_System_View)]
   public async Task<List<GetPatientReviewOfSystemsDataResponseDto>> GetPatientReviewOfSystemsData(long patientId) =>
        await _getPatientReviewOfSystemsDataQueryHandler.Handle(patientId);

   [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Review_of_System_Edit)]
   public async Task UpdatePatientReviewOfSystemsData(CreateReviewOfSystemsRequestDto request) =>
        await _updateReviewOfSystemsDataCommandHandler.Handle(request);

   [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_Review_of_System_Delete)]
   public async Task DeleteReviewOfSystemsData(long id) =>
        await _deleteReviewOfSystemsDataCommandHandler.Handle(id);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_OccupationHistory_Create)]
    public async Task SaveOccupationHistory(CreateOccupationalHistoryDto request) =>
        await _createOccupationalHistoryCommandHandler.Handle(request);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_OccupationHistory_View)]
    public async Task<List<CreateOccupationalHistoryDto>> GetPatientOccupationalHistory(long patientId) =>
        await _getPatientOccupationalHistoryRequestHandler.Handle(patientId);
    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_OccupationHistory_Delete)]
    public async Task DeletePatientOccupation(long id) =>
        await _deletePatientOccupationHistoryCommandHandler.Handle(id);

    [AbpAuthorize(AppPermissions.Pages_Patient_Profiles_OccupationHistory_Edit)]
    public async Task UpdatePatientOccupationHistory(CreateOccupationalHistoryDto request) =>
        await _updateOccupationalHistoryCommandHandler.Handle(request);

    public async Task<ReviewDetailsHistoryStateDto> GetReviewDetailsHistoryStateForPatient(long patientId) =>
        await _getReviewDetailsHistoryStateRequestHandler.Handle(patientId);

    public async Task UpdateReviewDetailsHistoryStates(ReviewDetailsHistoryStateDto request)
    {
        var user = await GetCurrentUserAsync();
        await _updateReviewDetailsHistoryStateCommandHandler.Handle(request, user.FullName);
    }
 
    public async Task DeletePastMedicalHistory(long id) =>
        await _deletePastMedicalHistoryCommandHandler.Handle(id);

    public async Task UpdatePastMedicalHistory(PatientPastMedicalConditionCommandRequest request) =>
        await _updatePastMedicalHistoryCommandHandler.Handle(request);

    public async Task UpdatePatientMajorInjury(CreatePatientMajorInjuryRequest request) =>
        await _updateMajorInjuryCommandHandler.Handle(request);

}
