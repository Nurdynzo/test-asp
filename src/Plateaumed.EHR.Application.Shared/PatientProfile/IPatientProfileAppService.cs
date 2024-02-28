using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.PatientProfile;

public interface IPatientProfileAppService : IApplicationService
{
    Task DeletePatientAllergy(long id);
    Task EditPatientAllergy(EditPatientAllergyCommandRequest request);
    Task CreatePatientAllergy(CreatePatientAllergyCommandRequest request);

    Task<List<GetPatientAllergyResponseDto>> GetPatientAllergies(long patientId);

    Task<List<PostmenopausalSymptomSuggestionResponse>> GetPostmenopausalSymptoms();
    Task<PatientFamilyHistoryDto> SavePatientFamilyHistory(PatientFamilyHistoryDto request);

    Task<PatientFamilyHistoryDto> GetPatientFamilyHistory(long patientId);

    Task SavePatientPastMedicalHistory(PatientPastMedicalConditionCommandRequest request);

    Task SavePatientMajorInjury(CreatePatientMajorInjuryRequest request);

    Task<List<GetContraceptionSuggestionQueryResponse>> GetContraceptionSuggestion();

    Task<List<PatientGynaecologicProcedureSuggestionResponse>> GetPatientGynaecologicProcedureSuggestion();
    
    Task<SaveMenstruationAndFrequencyCommand> SaveMenstruationAndFrequency(SaveMenstruationAndFrequencyCommand request);

    Task<List<GetPatientMenstrualFrequencyResponse>> GetPatientMenstrualFrequency(long patientId);

    Task<List<GetPatientMenstrualFlowResponse>> GetPatientMenstrualFlow(long patientId);
    Task DeleteMenstrualFrequency(long id);
    Task DeleteMenstrualFlow(long id);

    Task<GetPatientPastMedicalConditionQueryResponse> GetPatientPastMedicalHistory(long patientId);
    
    Task CreatePatientPhysicalExercise(CreatePhysicalExerciseCommandRequest request);

    Task<GetPatientPhysicalExerciseQueryResponse> GetPatientPhysicalExercise(long patientId);

    Task<SaveMenstrualBloodFlowCommandRequest> SaveMenstrualBloodFlow(SaveMenstrualBloodFlowCommandRequest request);

    Task<List<GetPatientGynaecologicalIllnessSuggestionQueryResponse>> GetPatientGynaecologicalIllnessSuggestion();

    Task SavePatientTravelHistory(List<CreatePatientTravelHistoryCommand> request);

    Task DeletePatientMajorInjury(long id);

    Task<GetPatientTravelHistoryQueryResponse> GetPatientTravelHistory(long patientId);

    Task<List<GetPatientMajorInjuryResponse>> GetPatientMajorInjury(long patientId);

    Task<List<GetChronicDiseaseSuggestionQueryResponse>> GetChronicDiseaseSuggestion();

    Task<List<GetAllergyTypeSuggestionQueryResponse>> GetAllergyTypeSuggestion();

    Task<List<GetAllergyExperiencedSuggestionQueryResponse>> GetAllergyExperiencedSuggestion();

    Task DeletePatientChronicConditions(long id);

    Task SavePatientGenotypeAndBloodGroup(UpdatePatientGenotypeAndBloodGroupCommandRequest request);

    Task<List<GetImplantSuggestionResponse>> GetImplantSuggestions();

    Task CreatePatientImplant(CreatePatientImplantCommandRequestDto request);

    Task<List<GetDiagnosisSuggestionResponseDto>> GetDiagnosisSuggestions();

    Task<List<GetProcedureSuggestionResponseDto>> GetProcedureSuggestions();

    Task<List<ReviewOfSystemsSuggestionResponseDto>> GetReviewOfSystemsSuggestions(SymptomsCategory category);

    Task<List<GetPatientImplantResponseDto>> GetPatientImplants(long patientId);

    Task AddBloodTransfusionHistory(CreateBloodTransfusionHistoryRequestDto request);

    Task AddSurgicalHistory(CreateSurgicalHistoryRequestDto request);

    Task<List<GetSurgicalHistoryResponseDto>> GetPatientSurgicalHistory(long patientId);

    Task<List<GetPatientBloodTransfusionHistoryResponseDto>> GetPatientBloodTransfusionHistory(long patientId);

    Task DeleteBloodTransfusionHistory(long id);

    Task DeleteSurgicalHistory(long id);

    Task UpdateSurgicalHistory(long id, CreateSurgicalHistoryRequestDto request);

    Task UpdateBloodTransfusionHistory(long id, CreateBloodTransfusionHistoryRequestDto request);

    Task CreateAlcoholHistory(CreateAlcoholHistoryRequestDto request);

    Task CreateRecreationalDrugHistory(CreateRecreationalDrugsHistoryRequestDto request);

    Task CreateCigeretteAndTobaccoHistory(CreateCigaretteHistoryRequestDto request);

    Task UpdateAlcoholHistory(CreateAlcoholHistoryRequestDto request);

    Task UpdateCigaretteHistory(CreateCigaretteHistoryRequestDto request);
    
    Task UpdateRecreationalDrugHistory(CreateRecreationalDrugsHistoryRequestDto request);

    Task<List<GetAlcoholHistoryResponseDto>> GetAlcoholHistory(long patientId);

    Task<List<GetRecreationalDrugHistoryResponseDto>> GetRecreationalDrugHistory(long patientId);

    Task<List<GetCigaretteHistoryResponseDto>> GetCigaretteAndTobaccoHistory(long patientId);

    Task<List<GetTobaccoSuggestionResponseDto>> GetCigeretteAndTobaccoSuggestion();

    Task<List<GetRecreationalDrugsSuggesionResponseDto>> GetRecreationalDrugSuggestions();

    Task<List<GetAlcoholTypesResponseDto>> GetAlcoholTypesSuggestion();

    Task DeleteAlcoholHistory(long historyId);

    Task DeleteCigaretteAndTobaccoHistory(long hisoryId);

    Task DeleteRecreationalDrugHistory(long historyId);

    Task CreateDrugHistory(CreateDrugHistoryRequestDto request);

    Task UpdateDrugHistory(CreateDrugHistoryRequestDto request);

    Task<List<GetDrugHistoryResponseDto>> GetDrugHistory(long patientId);

    Task DeleteDrugHistory(long id);

    Task<GetPatientBloodGroupAndGenotypeResponseDto> GetPatientBloodGroupAndGenotype(long patientId);

    Task UpdatePatientImplant(CreatePatientImplantCommandRequestDto request);

    Task DeletePatientImplant(long id);

    Task UpdateVaccinationHistory(CreateVaccinationHistoryDto request);

    Task DeleteVaccinationHistory(long id);

    Task<List<VaccinationSuggestionResponseDto>> GetVaccinationSuggestions();

    Task DeletePatientTravelHistory(long id);

    Task CreateReviewOfSystemsData(ReviewOfSystemsInputDto input);

    Task<List<GetPatientReviewOfSystemsDataResponseDto>> GetPatientReviewOfSystemsData(long patientId);

    Task UpdatePatientReviewOfSystemsData(CreateReviewOfSystemsRequestDto request);

    Task DeleteReviewOfSystemsData(long id);
    Task<List<AwaitClinicalInvestigationResultResponse>> GetAwaitingClinicalInvestigationResult(GetClinicalInvestigationQuery request);

    Task SaveOccupationHistory(CreateOccupationalHistoryDto request);

    Task<List<CreateOccupationalHistoryDto>> GetPatientOccupationalHistory(long patientId);
    Task DeletePatientOccupation(long id);

    Task UpdatePatientOccupationHistory(CreateOccupationalHistoryDto request);

    Task<ReviewDetailsHistoryStateDto> GetReviewDetailsHistoryStateForPatient(long patientId);

    Task UpdateReviewDetailsHistoryStates(ReviewDetailsHistoryStateDto request);
    Task<List<ClinicalInvestigationResultResponse>> GetClinicalInvestigation(GetClinicalInvestigationQuery request);

    Task DeletePatientFamilyMember(long id);

    Task UpdatePatienFamilyMemberDetails(UpdateFamilyMembersDto request);

    Task UpdatePatientFamilyHistory(PatientFamilyHistoryDto request);

    Task DeletePastMedicalHistory(long id);

    Task UpdatePastMedicalHistory(PatientPastMedicalConditionCommandRequest request);

    Task UpdatePatientMajorInjury(CreatePatientMajorInjuryRequest request);
}
