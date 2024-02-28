using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves;

[AbpAuthorize(AppPermissions.Pages_NurseReviewAndSave)]
public class NurseReviewAndSaveAppService : EHRAppServiceBase, INurseReviewAndSaveAppService
{
    private readonly IDoctorReviewAndSaveBaseQuery _baseQuery;
    private readonly ICreateNursingRecordCommandHandler _createNursingRedordCommandHandler;
    private readonly INursingNoteQueryHandler _nursingNoteCommandHandler;
    private readonly IAbpSession _abpSession; 


    public NurseReviewAndSaveAppService(
        IDoctorReviewAndSaveBaseQuery baseQuery,
        IAbpSession abpSession,
        ICreateNursingRecordCommandHandler createNursingRedordCommandHandler,
        INursingNoteQueryHandler nursingNoteCommandHandler)
    {
        _baseQuery = baseQuery;
        _abpSession = abpSession;
        _createNursingRedordCommandHandler = createNursingRedordCommandHandler;
        _nursingNoteCommandHandler = nursingNoteCommandHandler;
    }

    [AbpAuthorize(AppPermissions.Pages_NurseReviewAndSave_Create)]
    public async Task<NursingRecordDto> CreateNursingRecord(NursingRecordDto requestDto)
        => await _createNursingRedordCommandHandler.Handle(requestDto);

    public async Task<NursingRecordDto> GetNursingRecord(long encounterId)
    {
        if (encounterId == 0)
            throw new UserFriendlyException("Encounter Id is required.");

        var patientEncounter = await _baseQuery.GetPatientEncounter(encounterId, _abpSession.TenantId.GetValueOrDefault());

        var loginUser = await GetCurrentUserAsync();
        var staffUserId = loginUser.Id;
        return new NursingRecordDto()
        {
            EncounterId = encounterId,
            CreationTime = DateTime.Now,
            IsAutoSaved = false,
            NursingNote = await _nursingNoteCommandHandler.Handle(staffUserId, patientEncounter)
        };
    }
}
