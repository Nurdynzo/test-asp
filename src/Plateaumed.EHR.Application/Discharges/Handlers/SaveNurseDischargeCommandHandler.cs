using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.DoctorDischarge;

namespace Plateaumed.EHR.Discharges.Handlers;
public class SaveNurseDischargeCommandHandler : ISaveNurseDischargeCommandHandler
{
    private readonly IRepository<Discharge, long> _dischargeRepository;
    private readonly IRepository<DischargeNote, long> _noteRepository;
    private readonly IRepository<PatientCauseOfDeath, long> _causesOfDeathRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAbpSession _abpSession;
    private readonly IObjectMapper _objectMapper;


    public SaveNurseDischargeCommandHandler(IRepository<Discharge, long> dischargeRepository,
            IUnitOfWorkManager unitOfWorkManager,
            PatientAppointments.Abstractions.IBaseQuery appointmentBaseQuery,
            IAbpSession abpSession,
            IObjectMapper objectMapper,
            IRepository<DischargeNote, long> noteRepository,
            IRepository<PatientCauseOfDeath, long> causesOfDeathRepository)
    {
        _dischargeRepository = dischargeRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _abpSession = abpSession;
        _objectMapper = objectMapper;
        _noteRepository = noteRepository;
        _causesOfDeathRepository = causesOfDeathRepository;
    }
    public async Task<DischargeDto> Handle(FinalizeNurseDischargeDto requestDto)
    {
        if (requestDto.DischargeType != DischargeEntryType.NORMAL && requestDto.DischargeType != DischargeEntryType.DAMA
            && requestDto.DischargeType != DischargeEntryType.DECEASED && requestDto.DischargeType != DischargeEntryType.SUSPENSEADMISSION)
        {
            throw new UserFriendlyException("Discharge Type is required.");
        }
        var discharge = await _dischargeRepository.GetAll()
            .FirstOrDefaultAsync(s => s.Id == requestDto.Id.GetValueOrDefault() && s.TenantId == _abpSession.TenantId.GetValueOrDefault()) ??
            throw new UserFriendlyException("Discharge not found using the specified Id");

        //map request data and set other properties
        discharge.status = DischargeStatus.FINALIZED;
        discharge.IsFinalized = true;
        if (requestDto.DischargeType == DischargeEntryType.DECEASED)
        {
            discharge.IsBroughtInDead = requestDto.IsBroughtInDead;
            discharge.DateOfDeath = !requestDto.IsBroughtInDead ? requestDto.DateOfDeath : null;
            discharge.TimeOfDeath = !requestDto.IsBroughtInDead ? requestDto.TimeOfDeath : string.Empty;
            discharge.TimeCPRCommenced = !requestDto.IsBroughtInDead ? requestDto.TimeCPRCommenced : string.Empty;
            discharge.TimeCPREnded = !requestDto.IsBroughtInDead ? requestDto.TimeCPREnded : string.Empty;
        }
        else
        {
            discharge.IsBroughtInDead = false;
            discharge.DateOfDeath = null;
            discharge.TimeOfDeath = string.Empty;
            discharge.TimeCPRCommenced = string.Empty;
            discharge.TimeCPREnded = string.Empty;
        }

        if (!string.IsNullOrEmpty(requestDto.Note))
        {
            await _noteRepository.InsertAsync(new DischargeNote()
            {
                DischargeId = requestDto.Id.GetValueOrDefault(),
                Note = requestDto.Note
            });
        }

        discharge.PatientAssessment = requestDto.PatientAssessment;
        discharge.ActivitiesOfDailyLiving = requestDto.ActivitiesOfDailyLiving;
        discharge.Drugs = requestDto.Drugs;

        await _dischargeRepository.UpdateAsync(discharge);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        return await ReturnObject(discharge);
    }

    private async Task<DischargeDto> ReturnObject(Discharge model)
    {
        var causes = await _causesOfDeathRepository.GetAll().Where(s => s.CreatorUserId == model.LastModifierUserId && s.DischargeId == model.Id)
            .Select(t => new PatientCauseOfDeathDto()
            {
                CausesOfDeath = t.CausesOfDeath
            }).ToListAsync() ?? new List<PatientCauseOfDeathDto>();

        var notes = await _noteRepository.GetAll().Where(s => s.CreatorUserId == model.LastModifierUserId && s.DischargeId == model.Id)
            .Select(t => new DischargeNoteDto()
            {
                Note = t.Note
            }).ToListAsync() ?? new List<DischargeNoteDto>();

        return model == null
            ? new DischargeDto()
            : new DischargeDto
            {
                Id = model.Id,
                PatientId = model.PatientId,
                AppointmentId = model.AppointmentId,
                EncounterId = model.EncounterId,
                IsFinalized = model.IsFinalized,
                DischargeType = model.DischargeType,
                DischargeTypeStr = model.DischargeType.ToString(),
                CausesOfDeath = causes,
                Note = notes,
                status = model.status,
                StatusStr = model.status.ToString(),
                IsBroughtInDead = model.IsBroughtInDead,
                DateOfDeath = model.DateOfDeath,
                TimeOfDeath = model.TimeOfDeath,
                TimeCPRCommenced = model.TimeCPRCommenced,
                TimeCPREnded = model.TimeCPREnded,
                PatientAssessment = model.PatientAssessment,
                ActivitiesOfDailyLiving = model.ActivitiesOfDailyLiving,
                Drugs = model.Drugs,
                CreatedAt = model.CreationTime,
                ModifieddAt = model.LastModificationTime
            };
    }
}
