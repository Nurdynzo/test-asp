using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves;

[AbpAuthorize(AppPermissions.Pages_DoctorReviewAndSave)]
public class DoctorReviewAndSaveAppService : EHRAppServiceBase, IDoctorReviewAndSaveAppService
{
    private readonly IGenerateOnBehalfOfQueryHandler _getDoctorListQueryHandler;
    private readonly IDoctorsSummaryQueryHandler _doctorSummaryQueryHandler;
    private readonly IEncounterSummaryQueryHandler _encounterSummaryQueryHandler;
    private readonly INursesSummaryQueryHandler _nurseSummaryQueryHandler;
    private readonly IDoctorReviewAndSaveBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;


    public DoctorReviewAndSaveAppService(IGenerateOnBehalfOfQueryHandler getDoctorListQueryHandler, 
        IDoctorsSummaryQueryHandler doctorSummaryQueryHandler,
        IEncounterSummaryQueryHandler encounterSummaryQueryHandler,
        INursesSummaryQueryHandler nurseSummaryQueryHandler,
        IDoctorReviewAndSaveBaseQuery baseQuery,
        IAbpSession abpSession)
    {
        _getDoctorListQueryHandler = getDoctorListQueryHandler;
        _doctorSummaryQueryHandler = doctorSummaryQueryHandler;
        _encounterSummaryQueryHandler = encounterSummaryQueryHandler;
        _nurseSummaryQueryHandler = nurseSummaryQueryHandler;
        _baseQuery = baseQuery;
        _abpSession = abpSession;
    }

    public async Task<List<GetStaffMembersSimpleResponseDto>> GetOnBehalfOfList()
        => await _getDoctorListQueryHandler.Handle(GetCurrentUser().Id, GetCurrentUserFacilityId());


    public async Task<FirstVisitNoteDto> GetFirstVisitNote(long encounterId, long? doctorUserId)
    {
        if (encounterId == 0)
            throw new UserFriendlyException("Encounter Id is required.");

        var patientEncounter = await _baseQuery.GetPatientEncounter(encounterId, _abpSession.TenantId.GetValueOrDefault());
        var patient = patientEncounter?.Patient ??
            throw new UserFriendlyException("Patient not found.");


        var loginUser = await GetCurrentUserAsync();
        var staffUserId = doctorUserId == null ? loginUser.Id : doctorUserId.GetValueOrDefault();
        var isOnBehalfOf = doctorUserId == null ? true : false;
        return new FirstVisitNoteDto()
        {
            PatientId = patient.Id,
            EncounterId = encounterId,
            DateGenerated = DateTime.Now,
            DoctorSummary = await _doctorSummaryQueryHandler.Handle(staffUserId, isOnBehalfOf, patientEncounter),
            EncounterSummary = await _encounterSummaryQueryHandler.Handle(encounterId, patientEncounter),
            NurseSummary = await _nurseSummaryQueryHandler.Handle(encounterId)
        };
    }
}
