using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.DoctorDischarge;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.PatientAppointments.Abstractions;

namespace Plateaumed.EHR.Discharges.Handlers;

public class CreateDischargeCommandHandler : ICreateDischargeCommandHandler
{
    private readonly IRepository<Discharge, long> _dischargeRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IBaseQuery _appointmentBaseQuery;
    private readonly IEncounterManager _encounterManager;
    private readonly IRepository<PatientCauseOfDeath, long> _causeOfDeathRepository;
    private readonly IRepository<DischargeNote, long> _noteRepository;
    
    public CreateDischargeCommandHandler(IRepository<Discharge, long> dischargeRepository, 
        IUnitOfWorkManager unitOfWorkManager, IBaseQuery appointmentBaseQuery,
        IEncounterManager encounterManager,
        IRepository<PatientCauseOfDeath, long> causeOfDeathRepository, 
        IRepository<DischargeNote, long> noteRepository)
    {
        _dischargeRepository = dischargeRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _appointmentBaseQuery = appointmentBaseQuery;
        _encounterManager = encounterManager;
        _causeOfDeathRepository = causeOfDeathRepository;
        _noteRepository = noteRepository;
    }

    public async Task<DischargeDto> Handle(CreateDischargeDto requestDto)
    {
        if (requestDto.DischargeType != DischargeEntryType.NORMAL && requestDto.DischargeType != DischargeEntryType.DAMA
            && requestDto.DischargeType != DischargeEntryType.SUSPENSEADMISSION && requestDto.DischargeType != DischargeEntryType.DECEASED)
            throw new UserFriendlyException("Discharge Type is required.");

        if (requestDto.DischargeType is DischargeEntryType.NORMAL or DischargeEntryType.DAMA or DischargeEntryType.SUSPENSEADMISSION
            && (requestDto.Prescriptions == null || requestDto.Prescriptions.Count <= 0))
            throw new UserFriendlyException("At least one prescription data is required.");

        if (requestDto.DischargeType is DischargeEntryType.NORMAL or DischargeEntryType.DAMA or DischargeEntryType.SUSPENSEADMISSION
            && (requestDto.PlanItems == null || requestDto.PlanItems.Count <= 0))
            throw new UserFriendlyException("At least one plan item is required.");

        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        var patientId = requestDto.PatientId;

        //Validate appointmentId
        var isFound = requestDto.AppointmentId is > 0 && _appointmentBaseQuery
            .GetAppointmentsBaseQuery().Any(a => a.Patient.Id == patientId && a.Appointment.Id == requestDto.AppointmentId);

        //map request data and set other properties
        var discharge = MapObject(requestDto);
        discharge.AppointmentId = !isFound ? null : requestDto.AppointmentId;

        var id = await _dischargeRepository.InsertAndGetIdAsync(discharge);
        if(requestDto.DischargeType  == DischargeEntryType.DECEASED && !requestDto.IsBroughtInDead)
        {
            var causesOfDeath = requestDto.CausesOfDeath.Select(t => new PatientCauseOfDeath()
            {
                DischargeId = id,
                CausesOfDeath = t.CausesOfDeath
            }).ToList();
            foreach (var cause in causesOfDeath)
                await _causeOfDeathRepository.InsertAsync(cause);
        }
        if (!string.IsNullOrEmpty(requestDto.Note))
        {
            await _noteRepository.InsertAsync(new DischargeNote()
            {
                DischargeId = id,
                Note = requestDto.Note
            });
        }
        await UpdateEncounter(requestDto);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        return await ReturnObject(discharge);
    }

    private async Task UpdateEncounter(CreateDischargeDto requestDto)
    {
        if (requestDto.DischargeType == DischargeEntryType.DECEASED)
            await _encounterManager.MarkAsDeceased(requestDto.EncounterId);
        else
            await _encounterManager.RequestDischargePatient(requestDto.EncounterId);
    }

    private Discharge MapObject (CreateDischargeDto requestDto)
    {
        if (requestDto.DischargeType == DischargeEntryType.DECEASED)
        {
            return requestDto == null
            ? new Discharge()
            : new Discharge
            {
                Id = requestDto.Id.GetValueOrDefault(),
                PatientId = requestDto.PatientId,
                AppointmentId = requestDto.AppointmentId,
                IsFinalized = false,
                DischargeType = requestDto.DischargeType,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = requestDto.IsBroughtInDead,
                DateOfDeath = !requestDto.IsBroughtInDead ? requestDto.DateOfDeath : null,
                TimeOfDeath = !requestDto.IsBroughtInDead ? requestDto.TimeOfDeath : string.Empty,
                TimeCPRCommenced = !requestDto.IsBroughtInDead ? requestDto.TimeCPRCommenced : string.Empty,
                TimeCPREnded = !requestDto.IsBroughtInDead ? requestDto.TimeCPREnded : string.Empty,
                EncounterId = requestDto.EncounterId,
            };
        }
        else
        {
            return requestDto == null
            ? new Discharge()
            : new Discharge
            {
                Id = requestDto.Id.GetValueOrDefault(),
                PatientId = requestDto.PatientId,
                AppointmentId = requestDto.AppointmentId,
                IsFinalized = false,
                DischargeType = requestDto.DischargeType,
                status = DischargeStatus.INITIATED,
                IsBroughtInDead = false,
                DateOfDeath = null,
                TimeOfDeath = string.Empty,
                TimeCPRCommenced = string.Empty,
                TimeCPREnded = string.Empty,
                EncounterId = requestDto.EncounterId,
            };
        }
        
    }
    
    private async Task<DischargeDto> ReturnObject(Discharge model)
    {
        var causes = await _causeOfDeathRepository.GetAll().Where(s => s.CreatorUserId == model.CreatorUserId && s.DischargeId == model.Id)
            .Select(t => new PatientCauseOfDeathDto()
            {
                CausesOfDeath = t.CausesOfDeath
            }).ToListAsync() ??  new List<PatientCauseOfDeathDto>();

        var notes = await _noteRepository.GetAll().Where(s=>s.CreatorUserId == model.CreatorUserId && s.DischargeId == model.Id)
            .Select(t=> new DischargeNoteDto()
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
                IsFinalized = model.IsFinalized,
                DischargeType = model.DischargeType,
                DischargeTypeStr = model.DischargeType.ToString(),
                status = model.status,
                StatusStr = model.status.ToString(),
                IsBroughtInDead = model.IsBroughtInDead,
                DateOfDeath = model.DateOfDeath,
                TimeOfDeath = model.TimeOfDeath,
                TimeCPRCommenced = model.TimeCPRCommenced,
                TimeCPREnded = model.TimeCPREnded,
                EncounterId = model.EncounterId,
                Note = notes,
                CausesOfDeath = causes,
                CreatedAt = model.CreationTime
            };
    }
}
