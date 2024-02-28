using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Symptom;

namespace Plateaumed.EHR.Procedures;

public class ProcedureAppService : EHRAppServiceBase, IProcedureAppService
{
    private readonly IGetSnowmedSuggestionQueryHandler _getSnowmedSuggestionQueryHandler;
    private readonly ICreateProcedureCommandHandler _createProcedureCommandHandler;
    private readonly IGetPatientProceduresQueryHandler _getPatientProceduresQueryHandler;
    private readonly IEmailStatementCommandHandler _emailStatementCommandHandler;
    private readonly IGetSpecializedProcedureSafetyCheckListQueryHandler _getSpecializedProcedureSafetyCheckListQueryHandler;
    private readonly IUpdateSpecializedProcedureCheckListCommandHandler _updateSpecializedProcedureCheckListCommandHandler;
    private readonly IGetCheckedSafetyListQueryHandler _getCheckedSafetyListQueryHandler;
    private readonly IGetStatementOfPatientOrNextOfKinOrGuardianQueryHandler _getStatementOfPatientOrNextOfKinOrGuardianQueryHandler;
    private readonly ICreateStatementOfHealthProfessionalCommandHandler _createStatementOfHealthProfessionalCommandHandler;
    private readonly ICreateStatementOfPatientOrNextOfKinOrGuardianCommandHandler _statementOfPatientOrNextOfKinOrGuardianCommandHandler;
    private readonly IGetStatementOfHealthProfessionalQueryHandler _getStatementOfHealthProfessionalQueryHandler;
    private readonly ICreateSpecializedProcedureNurseDetailCommandHandler _createSpecializedProcedureNurseDetailCommandHandler;
    private readonly IDeleteSpecializedProcedureNurseDetailQueryHandler _deleteSpecializedProcedureNurseDetailQueryHandler;
    private readonly IGetSpecializedProcedureNurseDetailQueryHandler _getSpecializedProcedureNurseDetailQueryHandler;
    private readonly ISignConfirmationOfConsentCommandHandler _signConfirmationOfConsentCommandHandler;
    private readonly IGetSignConfirmationOfConsentQueryHandler _getSignConfirmationOfConsentQueryHandler;
    private readonly IMarkAsSpecializedCommandHandler _markAsSpecializedCommandHandler;
    private readonly IScheduleProcedureCommandHandler _scheduleProcedureCommandHandler;
    private readonly IGetAllStaffMembersQueryHandler _getAllStaffMembers;
    private readonly IGetOperatingRoomsQueryHandler _getOperatingRoomsQueryHandler;
    private readonly ICreateProcedureNoteCommandHandler _createProcedureNoteCommandHandler;
    private readonly ICreateAnaesthesiaNoteCommandHandler _createAnaesthesiaNoteCommandHandler;
    private readonly IGetNoteListQueryHandler _getNoteListQueryHandler;
    private readonly ICreateNoteTemplateCommandHandler _createNoteTemplateCommandHandler;
    private readonly IGetNoteTemplatesQueryHandler _getNoteTemplatesQueryHandler;
    private readonly IGetPatientInterventionsQueryHandler _getPatientInterventions;
    private readonly IUpdateStatusCommandHandler _updateStatusCommandHandler;
    private readonly IGetSpecializedProcedureOverviewQueryHandler _getSpecializedProcedureOverviewQueryHandler;
    private readonly IDeleteProcedureCommandHandler _deleteProcedureCommandHandler;

    public ProcedureAppService(
    IGetSnowmedSuggestionQueryHandler getSnowmedSuggestionQueryHandler,
    ICreateProcedureCommandHandler createProcedureCommandHandler,
    IGetPatientProceduresQueryHandler getPatientProceduresQueryHandler,
    IEmailStatementCommandHandler emailStatementCommandHandler,
    IGetSpecializedProcedureSafetyCheckListQueryHandler getSpecializedProcedureSafetyCheckListQueryHandler,
    IUpdateSpecializedProcedureCheckListCommandHandler updateSpecializedProcedureCheckListCommandHandler,
    IGetCheckedSafetyListQueryHandler getCheckedSafetyListQueryHandler,
    IGetStatementOfPatientOrNextOfKinOrGuardianQueryHandler getStatementOfPatientOrNextOfKinOrGuardianQueryHandler,
    ICreateStatementOfHealthProfessionalCommandHandler createStatementOfHealthProfessionalCommandHandler,
    ICreateSpecializedProcedureNurseDetailCommandHandler createSpecializedProcedureNurseDetailCommandHandler,
    IDeleteSpecializedProcedureNurseDetailQueryHandler deleteSpecializedProcedureNurseDetailQueryHandler,
    IGetSpecializedProcedureNurseDetailQueryHandler getSpecializedProcedureNurseDetailQueryHandler,
    ICreateStatementOfPatientOrNextOfKinOrGuardianCommandHandler statementOfPatientOrNextOfKinOrGuardianCommandHandler,
    IGetStatementOfHealthProfessionalQueryHandler getStatementOfHealthProfessionalQueryHandler,
    ISignConfirmationOfConsentCommandHandler signConfirmationOfConsentCommandHandler,
    IMarkAsSpecializedCommandHandler markAsSpecializedCommandHandler,
    IGetSignConfirmationOfConsentQueryHandler getSignConfirmationOfConsentQueryHandler,
    IGetAllStaffMembersQueryHandler getAllStaffMembers,
    IGetOperatingRoomsQueryHandler getOperatingRoomsQueryHandler, IScheduleProcedureCommandHandler scheduleProcedureCommandHandler,
    ICreateProcedureNoteCommandHandler createProcedureNoteCommandHandler,
    ICreateAnaesthesiaNoteCommandHandler createAnaesthesiaNoteCommandHandler,
    IGetNoteListQueryHandler getNoteListQueryHandler,
    ICreateNoteTemplateCommandHandler createNoteTemplateCommandHandler,
    IGetNoteTemplatesQueryHandler getNoteTemplatesQueryHandler,
    IGetPatientInterventionsQueryHandler getPatientInterventions,
    IGetSpecializedProcedureOverviewQueryHandler getSpecializedProcedureOverviewQueryHandler,
    IUpdateStatusCommandHandler updateStatusCommandHandler,
    IDeleteProcedureCommandHandler deleteProcedureCommandHandler)
    {
        _getSnowmedSuggestionQueryHandler = getSnowmedSuggestionQueryHandler;
        _createProcedureCommandHandler = createProcedureCommandHandler;
        _getPatientProceduresQueryHandler = getPatientProceduresQueryHandler;
        _emailStatementCommandHandler = emailStatementCommandHandler;
        _getSpecializedProcedureSafetyCheckListQueryHandler = getSpecializedProcedureSafetyCheckListQueryHandler;
        _updateSpecializedProcedureCheckListCommandHandler = updateSpecializedProcedureCheckListCommandHandler;
        _getCheckedSafetyListQueryHandler = getCheckedSafetyListQueryHandler;
        _getStatementOfPatientOrNextOfKinOrGuardianQueryHandler = getStatementOfPatientOrNextOfKinOrGuardianQueryHandler;
        _createStatementOfHealthProfessionalCommandHandler = createStatementOfHealthProfessionalCommandHandler;
        _createSpecializedProcedureNurseDetailCommandHandler = createSpecializedProcedureNurseDetailCommandHandler;
        _deleteSpecializedProcedureNurseDetailQueryHandler = deleteSpecializedProcedureNurseDetailQueryHandler;
        _getSpecializedProcedureNurseDetailQueryHandler = getSpecializedProcedureNurseDetailQueryHandler;
        _statementOfPatientOrNextOfKinOrGuardianCommandHandler = statementOfPatientOrNextOfKinOrGuardianCommandHandler;
        _getStatementOfHealthProfessionalQueryHandler = getStatementOfHealthProfessionalQueryHandler;
        _signConfirmationOfConsentCommandHandler = signConfirmationOfConsentCommandHandler;
        _markAsSpecializedCommandHandler = markAsSpecializedCommandHandler;
        _getSignConfirmationOfConsentQueryHandler = getSignConfirmationOfConsentQueryHandler;
        _getAllStaffMembers = getAllStaffMembers;
        _getOperatingRoomsQueryHandler = getOperatingRoomsQueryHandler;
        _scheduleProcedureCommandHandler = scheduleProcedureCommandHandler;
        _createProcedureNoteCommandHandler = createProcedureNoteCommandHandler;
        _createAnaesthesiaNoteCommandHandler = createAnaesthesiaNoteCommandHandler;
        _getNoteListQueryHandler = getNoteListQueryHandler;
        _createNoteTemplateCommandHandler = createNoteTemplateCommandHandler;
        _getNoteTemplatesQueryHandler = getNoteTemplatesQueryHandler;
        _getPatientInterventions = getPatientInterventions;
        _updateStatusCommandHandler = updateStatusCommandHandler;
        _getSpecializedProcedureOverviewQueryHandler = getSpecializedProcedureOverviewQueryHandler;
        _deleteProcedureCommandHandler = deleteProcedureCommandHandler;
    }

    public async Task<SpecializedProcedureSafetyCheckListDto> GetCheckedSafetyList(long patientId, long procedureId)
        => await _getCheckedSafetyListQueryHandler.Handle(patientId, procedureId);

    public async Task<SpecializedProcedureSafetyCheckListDto> GetSpecializedProcedureSafetyCheckList(long patientId, long procedureId)
        => await _getSpecializedProcedureSafetyCheckListQueryHandler.Handle(patientId, procedureId);
    public async Task UpdateSpecializedProcedureCheckList(SpecializedProcedureSafetyCheckListDto request)
        => await _updateSpecializedProcedureCheckListCommandHandler.Handle(request);
   public async Task DeleteProcedure(long input)
        => await _deleteProcedureCommandHandler.Handle(input);
    public async Task CreateProcedures(CreateProcedureDto input)
        => await _createProcedureCommandHandler.Handle(input);
    
    public async Task UpdateProcedureStatus(UpdateProcedureStatusDto input) 
        => await _updateStatusCommandHandler.Handle(input);

    public async Task CreateProcedureNote(CreateProcedureNoteDto input) =>
        await _createProcedureNoteCommandHandler.Handle(input);
    
    public async Task CreateAnaesthesiaNote(CreateAnaesthesiaNoteDto input) =>
        await _createAnaesthesiaNoteCommandHandler.Handle(input);
    
    public async Task CreateNoteTemplate(CreateNoteTemplateDto input) =>
        await _createNoteTemplateCommandHandler.Handle(input);

    public async Task<List<NoteResponseDto>> GetNoteList(long procedureId, string noteRequestType)
        => await _getNoteListQueryHandler.Handle(procedureId, noteRequestType);
    
    public async Task<List<NoteTemplateResponseDto>> GetNoteTemplatesList(string noteRequestType)
        => await _getNoteTemplatesQueryHandler.Handle(noteRequestType);
    
    public async Task<List<SnowstormSimpleResponseDto>> GetProcedureSuggestions() => 
        await _getSnowmedSuggestionQueryHandler.Handle(null, AllInputType.Procedure.ToString(), null); 
     
    public async Task<List<PatientProcedureResponseDto>> GetPatientProcedures(long patientId, string procedureType, long? parentProcedureId) 
        => await _getPatientProceduresQueryHandler.Handle(patientId, procedureType, parentProcedureId); 
    
    public async Task CreateStatementOfHealthProfessional(CreateStatementOfHealthProfessionalDto input) 
        => await _createStatementOfHealthProfessionalCommandHandler.Handle(input, AbpSession);
    
    public async Task<StatementOfHealthProfessionalResponseDto> GetStatementOfHealthProfessional(long procedureId) 
        => await _getStatementOfHealthProfessionalQueryHandler.Handle(procedureId, AbpSession); 
    
    public async Task EmailStatement(EmailStatementDto input) 
        => await _emailStatementCommandHandler.Handle(input, AbpSession); 
    
    public async Task CreateStatementOfPatientOrNextOfKinOrGuardian(CreateStatementOfPatientOrNextOfKinOrGuardianDto input) 
        => await _statementOfPatientOrNextOfKinOrGuardianCommandHandler.Handle(input, AbpSession);
    public async Task CreateSpecializedProcedureNurseDetail(CreateSpecializedProcedureNurseDetailCommand request)
        => await _createSpecializedProcedureNurseDetailCommandHandler.Handle(request).ConfigureAwait(false);
    public async Task DeleteSpecializedProcedureNurseDetail(long id)
        => await _deleteSpecializedProcedureNurseDetailQueryHandler.Handle(id);
    public async Task<GetSpecializedProcedureNurseDetailResponse> GetSpecializedProcedureNurseDetail(long procedureId)
        => await _getSpecializedProcedureNurseDetailQueryHandler.Handle(procedureId);
     
    public async Task<StatementOfPatientOrNextOfKinOrGuardianResponseDto> GetStatementOfPatientOrNextOfKinOrGuardian(long procedureId) 
        => await _getStatementOfPatientOrNextOfKinOrGuardianQueryHandler.Handle(procedureId, AbpSession); 
    
    public async Task SignConfirmationOfConsent(SignConfirmationOfConsentDto input) 
        => await _signConfirmationOfConsentCommandHandler.Handle(input, AbpSession); 
    
    public async Task<SignConfirmationOfConsentDto> GetSignConfirmationOfConsent(long procedureId) 
        => await _getSignConfirmationOfConsentQueryHandler.Handle(procedureId, AbpSession); 
    
    public async Task MarkProcedureAsSpecialized(CreateSpecializedProcedureDto input) 
        => await _markAsSpecializedCommandHandler.Handle(input, AbpSession);
    
    public async Task ScheduleProcedure(ScheduleProcedureDto input) 
        => await _scheduleProcedureCommandHandler.Handle(input, AbpSession);
 
    public async Task<List<GetStaffMembersSimpleResponseDto>> GetStaffMemberBySearchFilter(string filter, bool isAnaethetist = false)
        => await _getAllStaffMembers.SearchStaffHandle(filter.ToLower(), isAnaethetist, AbpSession);
    
    public async Task<List<OperatingRoomDto>> GetLocations()
        => await _getOperatingRoomsQueryHandler.GetAllOperatingRooms(AbpSession);

    public async Task<GetPatientInterventionsResponseDto> GetPatientInterventions(long patientId)
    {
        var facilityId = GetCurrentUserFacilityId();
        return await _getPatientInterventions.Handle(patientId, facilityId);
    }
    public async Task<GetSpecializedProcedureOverviewQueryResponse> GetSpecializedProcedureOverview(long patientId, long encounterId)
        => await _getSpecializedProcedureOverviewQueryHandler.Handle(patientId, encounterId);
}
