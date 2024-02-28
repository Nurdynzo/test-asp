using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.DoctorDischarge;
using Plateaumed.EHR.Medication.Abstractions;

namespace Plateaumed.EHR.Discharges.Handlers;
public class EditDischargeCommandHandler : IEditDischargeCommandHandler
{
    private readonly IRepository<Discharge, long> _dischargeRepository;
    private readonly IRepository<DischargeMedication, long> _dischargeMedicationRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly PatientAppointments.Abstractions.IBaseQuery _appointmentBaseQuery;
    private readonly IAbpSession _abpSession;
    private readonly IGetPatientMedicationQueryHandler _medicationQueryHandler;
    private readonly IGetDischargeMedicationsQueryHandler _getDischargeMedicationQueryHandler;
    private readonly IRepository<DischargePlanItem, long> _dischargePlanItemRepository;
    private readonly PlanItems.Abstractions.IBaseQuery _planItemsQueryHandler;
    private readonly IGetDischargePlanItemsQueryHandler _getDischargePlanItemIds;
    private readonly IRepository<PatientCauseOfDeath, long> _causeOfDeathRepository;
    private readonly IRepository<DischargeNote, long> _noteRepository;

    
    public EditDischargeCommandHandler(IRepository<Discharge, long> dischargeRepository,
            IRepository<DischargeMedication, long> dischargeMedicationRepository,
            IRepository<DischargePlanItem, long> dischargePlanItemRepository,
            PlanItems.Abstractions.IBaseQuery planItemsQueryHandler,
            IGetDischargePlanItemsQueryHandler getDischargePlanItemIds,
            IUnitOfWorkManager unitOfWorkManager, 
            PatientAppointments.Abstractions.IBaseQuery appointmentBaseQuery,
            IAbpSession abpSession,
            IGetPatientMedicationQueryHandler medicationQueryHandler,
            IGetDischargeMedicationsQueryHandler getDischargeMedicationQueryHandler,
            IRepository<PatientCauseOfDeath, long> causeOfDeathRepository,
            IRepository<DischargeNote, long> noteRepository)
    {
        _dischargeRepository = dischargeRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _appointmentBaseQuery = appointmentBaseQuery;
        _abpSession = abpSession;
        _medicationQueryHandler = medicationQueryHandler;
        _getDischargeMedicationQueryHandler = getDischargeMedicationQueryHandler;
        _dischargeMedicationRepository = dischargeMedicationRepository;
        _dischargePlanItemRepository = dischargePlanItemRepository;
        _planItemsQueryHandler = planItemsQueryHandler;
        _getDischargePlanItemIds = getDischargePlanItemIds;
        _causeOfDeathRepository = causeOfDeathRepository;
        _noteRepository = noteRepository;
    }
    public async Task<DischargeDto> Handle(CreateDischargeDto requestDto)
    {
        if (requestDto.DischargeType != DischargeEntryType.NORMAL && requestDto.DischargeType != DischargeEntryType.DAMA
            && requestDto.DischargeType != DischargeEntryType.DECEASED)
        {
            throw new UserFriendlyException("Discharge Type is required.");
        }

        if ((requestDto.DischargeType == DischargeEntryType.NORMAL || requestDto.DischargeType == DischargeEntryType.DAMA)
            && (requestDto.Prescriptions == null || requestDto.Prescriptions.Count <= 0))
        {
            throw new UserFriendlyException("At least one prescription data is required.");
        }

        if ((requestDto.DischargeType == DischargeEntryType.NORMAL || requestDto.DischargeType == DischargeEntryType.DAMA)
            && (requestDto.PlanItems == null || requestDto.PlanItems.Count <= 0))
        {
            throw new UserFriendlyException("At least one plan item is required.");
        }

        var patientId = (int)requestDto.PatientId;
        var dischargeId = requestDto.Id.GetValueOrDefault();
        var discharge = _dischargeRepository.GetAll()
                       .Where(s => s.PatientId == patientId && s.TenantId == _abpSession.TenantId && s.IsFinalized == false
                        && s.Id == dischargeId).FirstOrDefault();

        //validate appointmentId
        var isFound = (requestDto.AppointmentId != null || requestDto.AppointmentId > 0) ? _appointmentBaseQuery.GetAppointmentsBaseQuery()
            .Where(a => a.Patient.Id == patientId && a.Appointment.Id == requestDto.AppointmentId).Any() : false;

        //map request data and set other properties
        discharge.PatientId = requestDto.PatientId;
        discharge.IsFinalized = false;
        discharge.DischargeType = requestDto.DischargeType;
        discharge.status = DischargeStatus.INITIATED;
        discharge.IsBroughtInDead = requestDto.IsBroughtInDead;
        if (requestDto.DischargeType == DischargeEntryType.DECEASED)
        {
            discharge.DateOfDeath = !requestDto.IsBroughtInDead ? requestDto.DateOfDeath : null;
            discharge.TimeOfDeath = !requestDto.IsBroughtInDead ? requestDto.TimeOfDeath : string.Empty;
            discharge.TimeCPRCommenced = !requestDto.IsBroughtInDead ? requestDto.TimeCPRCommenced : string.Empty;
            discharge.TimeCPREnded = !requestDto.IsBroughtInDead ? requestDto.TimeCPREnded : string.Empty;
        }
        discharge.AppointmentId = !isFound ? null : requestDto.AppointmentId;
        discharge.LastModifierUserId = _abpSession.UserId;
        discharge.LastModificationTime = DateTime.UtcNow;

        await _dischargeRepository.UpdateAsync(discharge);

        if (!string.IsNullOrEmpty(requestDto.Note))
        {
            var doctorNote = await _noteRepository.GetAll().Where(s=>s.DischargeId == discharge.Id && s.CreatorUserId == discharge.CreatorUserId)
                .Select(s => new DischargeNote()
                {
                    Id = s.Id,
                    DischargeId = discharge.Id,
                    Note = requestDto.Note,
                }).FirstOrDefaultAsync();
            await _noteRepository.UpdateAsync(doctorNote);
        }

        await _unitOfWorkManager.Current.SaveChangesAsync();
        if(requestDto.Prescriptions != null && requestDto.Prescriptions.Count > 0)
        {
            var editMed = new EditDischargeMedicationDto()
            {
                DischargeId = discharge.Id,
                patientId = discharge.PatientId,
                Medication = requestDto.Prescriptions
            };
            await EditDischargeMedications(editMed);
        }

        if (requestDto.PlanItems != null && requestDto.PlanItems.Count > 0)
        {
            var editPlanItem = new EditDischargePlanItemDto()
            {
                DischargeId = discharge.Id,
                patientId = discharge.PatientId,
                PlanItems = requestDto.PlanItems
            };

            await EditDischargePlanItem(editPlanItem);
        }

        if (requestDto.DischargeType == DischargeEntryType.DECEASED && !requestDto.IsBroughtInDead)
        {
            await EditCauseOfDeath(dischargeId, requestDto.CausesOfDeath);
        }
        return ReturnObject(discharge);
    }
    private async Task<bool> EditDischargeMedications(EditDischargeMedicationDto requestDto)
    {
        var dischargeId = requestDto.DischargeId;
        var patientId = requestDto.patientId;
        var medicationDto = requestDto.Medication;

        if (medicationDto == null || medicationDto.Count == 0)
        {
            var dischargeMed = new List<DischargeMedication>();
            var patientMedications = await _medicationQueryHandler.Handle((int)patientId, _abpSession.TenantId);

            long[] inputList = medicationDto.Select(s => s.MedicationId).ToArray();
            var medicationsList = await _getDischargeMedicationQueryHandler.Handle(dischargeId);
            long[] existingList = medicationsList.Select(s => s.Id).ToArray();

            var skipList = existingList.Intersect(inputList).ToList(); //Return -> To be Skiped
            var deleteList = existingList.Except(inputList).ToList();  // Return -> To be deleted
            var insertList = inputList.Except(existingList).ToList(); // Return -> To be inserted

            var insertDischargeMedication = new List<DischargeMedication>();
            var deleteDischargeMedication = new List<DischargeMedication>();
            //Add the insert list
            foreach (var item in insertList)
            {
                //Validate the item
                var isValid = patientMedications.Where(s => s.PatientId == item && s.Id == item).Any();
                if (isValid)
                {
                    //Add item to list
                    insertDischargeMedication.Add(
                        new DischargeMedication
                        {
                            TenantId = _abpSession.TenantId.GetValueOrDefault(),
                            DischargeId = dischargeId,
                            MedicationId = item,
                            CreatorUserId = _abpSession.UserId,
                            CreationTime = DateTime.UtcNow
                        });
                }
            }
            //Remove the delete list
            foreach (var item in deleteList)
            {
                var toDeletePlanItems = medicationsList.Where(s => s.Id == item).Select(s => new DischargeMedication()
                {
                    TenantId = _abpSession.TenantId.GetValueOrDefault(),
                    DischargeId = dischargeId,
                    MedicationId = item
                }).FirstOrDefault();
                await _dischargeMedicationRepository.DeleteAsync(toDeletePlanItems);
            }

            foreach (var item in insertDischargeMedication)
            {
                await _dischargeMedicationRepository.InsertAsync(item);
            }

            await _unitOfWorkManager.Current.SaveChangesAsync();
            return true;
        }
        return false;
        
    }
    private async Task<bool> EditDischargePlanItem(EditDischargePlanItemDto requestDto)
    {
        var dischargeId = requestDto.DischargeId;
        var patientId = requestDto.patientId;
        var planItemDto = requestDto.PlanItems;
        if (planItemDto == null || planItemDto.Count == 0)
        {
            var patientPlanItems = _planItemsQueryHandler.GetPatientPlanItemsBaseQuery((int)patientId, _abpSession.TenantId);
            long[] inputList = planItemDto.Select(s => s.PlanItemId).ToArray();
            var planItems = await _getDischargePlanItemIds.Handle(dischargeId);
            long[] existingList = planItems.Select(s => s.PlanItemId).ToArray();

            var skipList = existingList.Intersect(inputList).ToList(); //Return -> To be Skiped
            var deleteList = existingList.Except(inputList).ToList();  // Return -> To be deleted
            var insertList = inputList.Except(existingList).ToList(); // Return -> To be inserted

            var insertDischargePlanItem = new List<DischargePlanItem>();
            var deleteDischargePlanItem = new List<DischargePlanItem>();
            //Add the insert list
            foreach (var item in insertList)
            {
                //Validate the item
                var isValid = patientPlanItems.Where(s => s.PatientId == item && s.Id == item).Any();
                if (isValid)
                {
                    //Add item to list
                    insertDischargePlanItem.Add(
                        new DischargePlanItem
                        {
                            TenantId = _abpSession.TenantId.GetValueOrDefault(),
                            DischargeId = dischargeId,
                            PlanItemId = item,
                            CreatorUserId = _abpSession.UserId,
                            CreationTime = DateTime.UtcNow
                        });
                }
            }
            //Remove the delete list
            foreach (var item in deleteList)
            {
                var toDeletePlanItems = planItems.Where(s => s.PlanItemId == item).Select(s => new DischargePlanItem()
                {
                    TenantId = _abpSession.TenantId.GetValueOrDefault(),
                    DischargeId = dischargeId,
                    PlanItemId = item
                }).FirstOrDefault();
                await _dischargePlanItemRepository.DeleteAsync(toDeletePlanItems);
            }

            foreach (var item in insertDischargePlanItem)
                await _dischargePlanItemRepository.InsertAsync(item);

            await _unitOfWorkManager.Current.SaveChangesAsync();
            return true;
        }

        return false;
    }
    private async Task<bool> EditCauseOfDeath(long dischargeId, List<PatientCauseOfDeathDto> causes)
    {
        if(causes.Count > 0)
        {
            var causesOfDeath = causes.Select(t => new PatientCauseOfDeath()
            {
                DischargeId = dischargeId,
                CausesOfDeath = t.CausesOfDeath
            }).ToList();
            string[] inputList = causesOfDeath.Select(s => s.CausesOfDeath).ToArray();
            var entries = await _causeOfDeathRepository.GetAll().Where(s => s.DischargeId == dischargeId).ToListAsync();
            string[] existingList = entries.Select(s => s.CausesOfDeath).ToArray();

            var skipList = existingList.Intersect(inputList).ToList(); //Return -> To be Skiped
            var deleteList = existingList.Except(inputList).ToList();  // Return -> To be deleted
            var insertList = inputList.Except(existingList).ToList(); // Return -> To be inserted
            var insertCauses = new List<PatientCauseOfDeath>();
            var deleteCauses = new List<PatientCauseOfDeath>();
            //Add the insert list
            foreach (var item in insertList)
            {
                //Validate the item
                insertCauses.Add(
                    new PatientCauseOfDeath
                    {
                        DischargeId = dischargeId,
                        CausesOfDeath = item
                    });
            }
            //Remove the delete list
            foreach (var item in deleteList)
            {
                await _causeOfDeathRepository.DeleteAsync(new PatientCauseOfDeath()
                {
                    DischargeId = dischargeId,
                    CausesOfDeath = item
                });
            }

            if (insertCauses.Count > 0)
                await _causeOfDeathRepository.InsertRangeAsync(insertCauses);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            return true;
        }
        return false;

    }
    private DischargeDto ReturnObject(Discharge model)
    {
        var causes = _causeOfDeathRepository.GetAll().Where(s => s.CreatorUserId == model.CreatorUserId && s.DischargeId == model.Id)
            .Select(t => new PatientCauseOfDeathDto()
            {
                CausesOfDeath = t.CausesOfDeath
            }).ToList() ?? new List<PatientCauseOfDeathDto>();

        var notes = _noteRepository.GetAll().Where(s => s.CreatorUserId == model.CreatorUserId && s.DischargeId == model.Id)
            .Select(t => new DischargeNoteDto()
            {
                Note = t.Note
            }).ToList() ?? new List<DischargeNoteDto>();

        return model == null
            ? new DischargeDto()
            : new DischargeDto
            {
                Id = model.Id,
                PatientId = model.PatientId,
                AppointmentId = model.AppointmentId,
                IsFinalized = model.IsFinalized,
                DischargeType = model.DischargeType,
                status = model.status,
                IsBroughtInDead = model.IsBroughtInDead,
                DateOfDeath = model.DateOfDeath,
                TimeOfDeath = model.TimeOfDeath,
                TimeCPRCommenced = model.TimeCPRCommenced,
                TimeCPREnded = model.TimeCPREnded,
                EncounterId = model.EncounterId,
                Note = notes,
                CausesOfDeath = causes
            };
    }
}


