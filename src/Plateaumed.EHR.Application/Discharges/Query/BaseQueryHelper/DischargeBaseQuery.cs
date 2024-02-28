using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Discharges.Query.BaseQueryHelper;

public class DischargeBaseQuery : IDischargeBaseQuery
{
    private readonly IRepository<Discharge, long> _dischargeRepository;
    private readonly IRepository<DischargeMedication, long> _dischargeMedicationRepository;
    private readonly IRepository<DischargePlanItem, long> _dischargePlanItemRepository;
    private readonly IRepository<PatientCauseOfDeath, long> _causesOfDeathRepository;
    private readonly IRepository<DischargeNote, long> _noteRepository;
    private readonly Medication.Abstractions.IBaseQuery _medicationQueryHandler;
    private readonly PlanItems.Abstractions.IBaseQuery _planItemQueryHandler;

    public DischargeBaseQuery(IRepository<Discharge, long> dischargeRepository, 
        IRepository<DischargeMedication, long> dischargeMedicationRepository,
        IRepository<DischargePlanItem, long> productCategoryRepository,
        Medication.Abstractions.IBaseQuery medicationQueryHandler,
        PlanItems.Abstractions.IBaseQuery planItemQueryHandler,
        IRepository<PatientCauseOfDeath, long> causesOfDeathRepository,
        IRepository<DischargeNote, long> noteRepository)
    {
        _dischargeRepository = dischargeRepository;
        _dischargeMedicationRepository = dischargeMedicationRepository;
        _dischargePlanItemRepository = productCategoryRepository;
        _medicationQueryHandler = medicationQueryHandler;
        _planItemQueryHandler = planItemQueryHandler;
        _causesOfDeathRepository = causesOfDeathRepository;
        _noteRepository = noteRepository;
    }

    public async Task<List<DischargeDto>> GetPatientDischarges(long patientId)
    {
        var discharges = await _dischargeRepository.GetAll().Where(s => s.PatientId == patientId).ToListAsync();
        var list = new List<DischargeDto>();
        foreach (var item in discharges)
            list.Add(await GetDischargeInformation(item.Id));
        return list;
    }
    public async Task<DischargeDto> GetPatientDischargeWithEncounterId(long patientId, long encounterId)
    {
        var discharge = await _dischargeRepository.GetAll()
            .FirstOrDefaultAsync(s => s.PatientId == patientId && s.EncounterId == encounterId);
        return discharge == null ? null : await GetDischargeInformation(discharge.Id);
    }
    public async Task<DischargeDto> GetDischargeInformation(long dischargeId)
    {
        var causes = await _causesOfDeathRepository.GetAll().Where(c => c.DischargeId == dischargeId).ToListAsync();
        var notes = await _noteRepository.GetAll().Where(c => c.DischargeId == dischargeId).ToListAsync();
        var prescriptions = await GetDischargeMedications(dischargeId).ToListAsync();
        var planItems = await GetDischargePlanItem(dischargeId).ToListAsync();
        var result =
            dischargeId == 0 ? null :
            await _dischargeRepository.GetAll()
            .Where(s => s.Id == dischargeId).Select(s => new DischargeDto
            {
                Id = s.Id,
                PatientId = s.PatientId,
                IsFinalized = s.IsFinalized,
                DischargeType = s.DischargeType,
                status = s.status,
                DischargeTypeStr = s.DischargeType.ToString(),
                StatusStr = s.status.ToString(),
                IsBroughtInDead = s.IsBroughtInDead,
                DateOfDeath = s.DateOfDeath,
                TimeOfDeath = s.TimeOfDeath,
                TimeCPRCommenced = s.TimeCPRCommenced,
                TimeCPREnded = s.TimeCPREnded,
                Prescriptions = prescriptions,
                PlanItems = planItems,
                CausesOfDeath = causes.Select(cause => new PatientCauseOfDeathDto()
                {
                    CausesOfDeath = cause.CausesOfDeath
                }).ToList() ?? new List<PatientCauseOfDeathDto>(),
                Note = notes.Select(note => new DischargeNoteDto()
                {
                    Note = note.Note
                }).ToList() ?? new List<DischargeNoteDto>(),
                AppointmentId = s.AppointmentId,
                EncounterId = s.EncounterId,
                PatientAssessment = s.PatientAssessment,
                ActivitiesOfDailyLiving = s.ActivitiesOfDailyLiving,
                Drugs = s.Drugs,
                CreatedAt = s.CreationTime,
                ModifieddAt = s.LastModificationTime
            }).FirstOrDefaultAsync();

        return result;
    }
    private string GetDrugs(long dischargeId)
    {
        var medications = GetDischargeMedications(dischargeId).Select(p => $"{p.ProductName} - {p.Direction}").ToList();
        return $"Prescribed drugs: {String.Join(",", medications)}";
    }

    public IQueryable<PatientMedicationForReturnDto> GetDischargeMedications(long dischargeId)
    {
        var query = _dischargeMedicationRepository.GetAll().Where(s=>s.DischargeId == dischargeId).
                     Select(s=> new DischargeMedicationDto
                     {
                         MedicationId = s.MedicationId,
                         DischargeId = dischargeId
                     });

        var discharge = _dischargeRepository.GetAll().FirstOrDefault(s => s.Id == dischargeId);
        long patientId = discharge != null ? discharge.PatientId : 0;
        var result = (from q in query
                     join medication in _medicationQueryHandler.GetPatientMedications((int)patientId) on q.MedicationId equals medication.Id 
                     select new PatientMedicationForReturnDto
                     {
                         Id = q.MedicationId,
                         PatientId = patientId,
                         ProductId = medication.ProductId,
                         ProductName = medication.ProductName,
                         ProductSource = medication.ProductSource,
                         DoseUnit = medication.DoseUnit,
                         Frequency = medication.Frequency,
                         Duration = medication.Duration,
                         Direction = medication.Direction,
                         Note = medication.Note
                     });

        return result;
    }

    public IQueryable<DischargePlanItemDto> GetDischargePlanItem(long dischargeId)
    {
        var query = (from discharge in _dischargeRepository.GetAll()
                     join dischargePlanItem in _dischargePlanItemRepository.GetAll() on discharge.Id equals dischargePlanItem.DischargeId

                     where dischargePlanItem.DischargeId == dischargeId
                     select new DischargePlanItemDto
                     {
                         PlanItemId = dischargePlanItem.PlanItemId,
                         PatientId = discharge.PatientId
                     });

        long patientId = query.Select(s => s.PatientId).FirstOrDefault();
        var result = (from q in query
                      join planItem in _planItemQueryHandler.GetPatientPlanItemsBaseQuery((int)patientId) on q.PatientId equals planItem.PatientId
                      select new DischargePlanItemDto
                      {
                          PlanItemId = q.PlanItemId,
                          PatientId = q.PatientId,
                          DischargeId = dischargeId,
                          Description = planItem.Description,
                          CreationTime = planItem.CreationTime,
                          DeletionTime = planItem.DeletionTime
                      });

        return result;
    }
    public NormalDischargeReturnDto MapNormalDischarge(DischargeDto discharge)
    {
        return new NormalDischargeReturnDto
        {
            Id = discharge.Id,
            PatientId = discharge.PatientId,
            IsFinalized = discharge.IsFinalized,
            DischargeType = discharge.DischargeType,
            DischargeTypeStr = discharge.DischargeType.ToString(),
            status = discharge.status,
            StatusStr = discharge.status.ToString(),
            AppointmentId = discharge.AppointmentId,
            EncounterId = discharge.EncounterId,
            CreatedAt = discharge.CreatedAt,
            ModifieddAt = discharge.ModifieddAt
        };
    }
    public MarkAsDeceaseDischargeReturnDto MapMarkAsDeceaseDischargeReturn(DischargeDto discharge)
    {
        return new MarkAsDeceaseDischargeReturnDto
        {
            Id = discharge.Id,
            PatientId = discharge.PatientId,
            IsFinalized = discharge.IsFinalized,
            DischargeType = discharge.DischargeType,
            DischargeTypeStr = discharge.DischargeType.ToString(),
            status = discharge.status,
            StatusStr = discharge.status.ToString(),
            EncounterId = discharge.EncounterId,
            IsBroughtInDead = discharge.IsBroughtInDead,
            CreatedAt = discharge.CreatedAt,
            ModifieddAt = discharge.ModifieddAt
        };
    }
}
