using Abp.Authorization;
using Abp.UI;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Encounters.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Discharges
{
    [AbpAuthorize(AppPermissions.Pages_Discharge)]
    public class DischargeAppService : EHRAppServiceBase, IDischargeAppService
    {
        private readonly ICreateDischargeCommandHandler _createDischargeCommandHandler;
        private readonly IEditDischargeCommandHandler _editDischargeCommandHandler;
        private readonly IGetDischargeMedicationsQueryHandler _getDischargeMedicationQueryHandler;
        private readonly IGetDischargePlanItemsQueryHandler _getDischargePlanItemsHandler;
        private readonly IGetDischargeByIdQueryHandler _getDischargeQueryHandler;
        private readonly IGetPatientDischargeQueryHandler _getPatientDischargeQueryHandler;
        private readonly IGetPatientEncounterQueryHandler _encounterHandler;
        private readonly ICreateDischargedMedicationsHandler _createDischargeMedicationHandler;
        private readonly ICreateDischargedPlanItemsHandler _createDischargePlanItemHandler;
        private readonly IDischargeBaseQuery _baseQueryHandler;


        public DischargeAppService(ICreateDischargeCommandHandler createDischargeCommandHandler,
            IEditDischargeCommandHandler editDischargeCommandHandle,
            IGetDischargeMedicationsQueryHandler getDischargeMedicationQueryHandler,
            IGetDischargePlanItemsQueryHandler getDischargePlanItemsHandler,
            IGetDischargeByIdQueryHandler getDischargeQueryHandler,
            IGetPatientDischargeQueryHandler getPatientDischargeQueryHandler,
            IGetPatientEncounterQueryHandler encounterHandler,
            ICreateDischargedMedicationsHandler createDischargeMedicationHandler,
            ICreateDischargedPlanItemsHandler createDischargePlanItemHandler,
            IDischargeBaseQuery baseQueryHandler
            )
        {
            _createDischargeCommandHandler = createDischargeCommandHandler;
            _editDischargeCommandHandler = editDischargeCommandHandle;
            _getDischargeMedicationQueryHandler = getDischargeMedicationQueryHandler;
            _getDischargePlanItemsHandler = getDischargePlanItemsHandler;
            _getDischargeQueryHandler = getDischargeQueryHandler;
            _getPatientDischargeQueryHandler = getPatientDischargeQueryHandler;
            _encounterHandler = encounterHandler;
            _createDischargeMedicationHandler = createDischargeMedicationHandler;
            _createDischargePlanItemHandler = createDischargePlanItemHandler;
            _baseQueryHandler = baseQueryHandler;
        }


        [AbpAuthorize(AppPermissions.Pages_Discharge_Create)]
        public async Task<NormalDischargeReturnDto> CreateOrEditNormalDischarge(CreateNormalDischargeDto input)
        {
            if (input.EncounterId == 0)
                throw new UserFriendlyException("Encounter Id is required.");

            var patientEncounter = await _encounterHandler.Handle(input.EncounterId);
            var patient = patientEncounter?.Patient ??
                throw new UserFriendlyException("Patient not found.");

            if (input.DischargeType != DoctorDischarge.DischargeEntryType.NORMAL && input.DischargeType != DoctorDischarge.DischargeEntryType.DAMA
                && input.DischargeType != DoctorDischarge.DischargeEntryType.SUSPENSEADMISSION)
                throw new UserFriendlyException("Invalid Discharge Type.");

            
            var request = new CreateDischargeDto()
            {
                Id = input.Id,
                PatientId = patientEncounter.PatientId,
                DischargeType = input.DischargeType,
                Status = DoctorDischarge.DischargeStatus.INITIATED,
                Prescriptions = input.Prescriptions,
                PlanItems = input.PlanItems,
                Note = input.Note,
                EncounterId = input.EncounterId,
                AppointmentId = input.AppointmentId
            };
            var discharge = new DischargeDto();
            if (input.Id > 0)
                discharge = await _editDischargeCommandHandler.Handle(request);

            discharge = await _createDischargeCommandHandler.Handle(request);
            if(request.Prescriptions.Count > 0)
                discharge.Prescriptions = await _createDischargeMedicationHandler.Handle(request.Prescriptions, discharge.Id, patientEncounter.PatientId);
            if (request.PlanItems.Count > 0)
                discharge.PlanItems = await _createDischargePlanItemHandler.Handle(request.PlanItems, discharge.Id, patientEncounter.PatientId);

            return _baseQueryHandler.MapNormalDischarge(discharge);
        }

        [AbpAuthorize(AppPermissions.Pages_Discharge_Create)]
        public async Task<MarkAsDeceaseDischargeReturnDto> CreateOrEditMarkAsDeceaseDischarge(CreateMarkAsDeceasedDischargeDto input)
        {
            if (input.EncounterId == 0)
                throw new UserFriendlyException("Encounter Id is required.");

            var patientEncounter = await _encounterHandler.Handle(input.EncounterId);
            var patient = patientEncounter?.Patient ??
                throw new UserFriendlyException("Patient not found.");

            var request = new CreateDischargeDto()
            {
                Id = input.Id,
                PatientId = patientEncounter.PatientId,
                DischargeType = DoctorDischarge.DischargeEntryType.DECEASED,
                Status = DoctorDischarge.DischargeStatus.INITIATED,
                Note = input.Note,
                EncounterId = input.EncounterId,
                AppointmentId = patientEncounter.AppointmentId,
                IsBroughtInDead = input.IsBroughtInDead,
                DateOfDeath = !input.IsBroughtInDead ? input.DateOfDeath : null,
                TimeOfDeath = !input.IsBroughtInDead ? input.TimeOfDeath : null,
                TimeCPRCommenced = !input.IsBroughtInDead ? input.TimeCPRCommenced : null,
                TimeCPREnded = !input.IsBroughtInDead ? input.TimeCPREnded : null,
                CausesOfDeath = !input.IsBroughtInDead ? input.CausesOfDeath : null
            };

            var discharge = new DischargeDto();
            if (input.Id > 0)
            {
                discharge = await _editDischargeCommandHandler.Handle(request);
            }

            discharge = await _createDischargeCommandHandler.Handle(request);

            return _baseQueryHandler.MapMarkAsDeceaseDischargeReturn(discharge);
        }

        public async Task<List<PatientMedicationForReturnDto>> GetDischargeMedications(int dischargeId)
        {
            return await _getDischargeMedicationQueryHandler.Handle(dischargeId);
        }
        public async Task<List<DischargePlanItemDto>> GetDischargePlanItems(int dischargeId)
        {
            return await _getDischargePlanItemsHandler.Handle(dischargeId);
        }
        public async Task<DischargeDto> GetDischargeById(int dischargeId)
        {
            return await _getDischargeQueryHandler.Handle(dischargeId);
        }
        public async Task<List<DischargeDto>> GetPatientDischarge(int patientId)
        {
            return await _getPatientDischargeQueryHandler.Handle(patientId);
        }

    }
}
