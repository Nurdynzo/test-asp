using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Discharges.Handlers;
public class CreateDischargedMedicationsHandler : ICreateDischargedMedicationsHandler
{
    private readonly IRepository<DischargeMedication, long> _dischargeMedicationRepository;
    private readonly IGetPatientMedicationQueryHandler _medicationQueryHandler;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAbpSession _abpSession;

    public CreateDischargedMedicationsHandler(IUnitOfWorkManager unitOfWorkManager,
            IGetPatientMedicationQueryHandler medicationQueryHandler,
            IAbpSession abpSession,
            IRepository<DischargeMedication, long> dischargeMedicationRepository)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _medicationQueryHandler = medicationQueryHandler;
        _dischargeMedicationRepository = dischargeMedicationRepository;
        _abpSession = abpSession;
    }
    public async Task<List<PatientMedicationForReturnDto>> Handle(List<CreateDischargeMedicationDto> requestDto, long dischargeId, long patientId)
    {
        if (dischargeId == 0)
            throw new UserFriendlyException("Discharge Id is required.");

        if (patientId == 0)
            throw new UserFriendlyException("PatientId Id is required.");

        if (requestDto!= null && requestDto.Count > 0)
        {
            var dischargeMed = new List<DischargeMedication>();
            var returnMedications = new List<PatientMedicationForReturnDto>();
            var patientMedications = await _medicationQueryHandler.Handle((int)patientId);
            foreach (var item in requestDto)
            {
                //Validate the item
                var medications = patientMedications.Where(s => s.Id == item.MedicationId).FirstOrDefault();
                if (medications != null && medications.Id > 0)
                {
                    //Add item to list
                    dischargeMed.Add(
                        new DischargeMedication
                        {
                            TenantId = _abpSession.TenantId.GetValueOrDefault(),
                            DischargeId = dischargeId,
                            MedicationId = item.MedicationId,
                            CreatorUserId = _abpSession.UserId,
                            CreationTime = DateTime.UtcNow
                        });
                    returnMedications.Add(medications);
                }
            }

            if (dischargeMed.Count > 0)
            {
                foreach (var item in dischargeMed)
                    await _dischargeMedicationRepository.InsertAndGetIdAsync(item);
                await _unitOfWorkManager.Current.SaveChangesAsync();
                return returnMedications;
            }
        }
        return null;
    }
}


