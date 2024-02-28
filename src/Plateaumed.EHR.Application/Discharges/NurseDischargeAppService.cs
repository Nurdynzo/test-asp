using Abp.Authorization;
using Abp.UI;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Encounters.Abstractions;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Discharges
{
    [AbpAuthorize(AppPermissions.Pages_Discharge)]
    public class NurseDischargeAppService : EHRAppServiceBase, INurseDischargeAppService
    {
        private readonly ISaveNurseDischargeCommandHandler _saveDischargeCommandHandler;
        private readonly IDischargeBaseQuery _baseQueryHandler;
        private readonly IGetPatientEncounterQueryHandler _encounterCreateHandler;
        
        public NurseDischargeAppService(ISaveNurseDischargeCommandHandler saveDischargeCommandHandler,
            IDischargeBaseQuery baseQueryHandler,
            IGetPatientEncounterQueryHandler encounterCreateHandler
            )
        {
            _saveDischargeCommandHandler = saveDischargeCommandHandler;
            _baseQueryHandler = baseQueryHandler;
            _encounterCreateHandler = encounterCreateHandler;
        }

        [AbpAuthorize(AppPermissions.Pages_Discharge_Finalize)]
        public async Task<NormalDischargeReturnDto> FinalizeNormalDischarge(FinalizeNormalDischargeDto input)
        {
            if (input.DischargeType != DoctorDischarge.DischargeEntryType.NORMAL && input.DischargeType != DoctorDischarge.DischargeEntryType.DAMA
                && input.DischargeType != DoctorDischarge.DischargeEntryType.SUSPENSEADMISSION)
                throw new UserFriendlyException("Invalid Discharge Type.");

            var discharge = await _baseQueryHandler.GetDischargeInformation(input.DischargeId);

            if (discharge == null)
                throw new UserFriendlyException("Discharge has to be initiated by a doctor. No discharge information found.");
            var request = new FinalizeNurseDischargeDto()
            {
                Id = input.DischargeId,
                PatientId = discharge.PatientId,
                DischargeType = input.DischargeType,
                Status = DoctorDischarge.DischargeStatus.FINALIZED,
                DischargeTypeStr = input.DischargeType.ToString(),
                StatusStr = DoctorDischarge.DischargeStatus.FINALIZED.ToString(),
                Note = input.Note,
                PatientAssessment = input.PatientAssessment,
                ActivitiesOfDailyLiving = input.ActivitiesOfDailyLiving,
                Drugs = input.Drugs,
                EncounterId = discharge.EncounterId.GetValueOrDefault(),
            };

            var response = await _saveDischargeCommandHandler.Handle(request);

            return _baseQueryHandler.MapNormalDischarge(discharge);
        }

        [AbpAuthorize(AppPermissions.Pages_Discharge_Finalize)]
        public async Task<MarkAsDeceaseDischargeReturnDto> FinalizeMarkAsDeceased(FinalizeMarkAsDeceaseDischargeDto input)
        {
            var discharge = await _baseQueryHandler.GetDischargeInformation(input.DischargeId);

            if (discharge == null)
                throw new UserFriendlyException("Discharge has to be initiated by a doctor. No discharge information found.");

            var request = new FinalizeNurseDischargeDto()
            {
                Id = discharge.Id,
                PatientId = discharge.PatientId,
                DischargeType = DoctorDischarge.DischargeEntryType.DECEASED,
                Status = DoctorDischarge.DischargeStatus.FINALIZED,
                DischargeTypeStr = DoctorDischarge.DischargeEntryType.DECEASED.ToString(),
                StatusStr = DoctorDischarge.DischargeStatus.FINALIZED.ToString(),
                Note = input.Note,
                EncounterId = discharge.EncounterId.GetValueOrDefault(),
                IsBroughtInDead = input.IsBroughtInDead,
                DateOfDeath = !input.IsBroughtInDead ? input.DateOfDeath : null,
                TimeOfDeath = !input.IsBroughtInDead ? input.TimeOfDeath : null,
                TimeCPRCommenced = !input.IsBroughtInDead ? input.TimeCPRCommenced : null,
                TimeCPREnded = !input.IsBroughtInDead ? input.TimeCPREnded : null,
                CausesOfDeath = !input.IsBroughtInDead ? input.CausesOfDeath : null,
            };

            var response = await _saveDischargeCommandHandler.Handle(request);

            return _baseQueryHandler.MapMarkAsDeceaseDischargeReturn(discharge);
        }
        public async Task<DischargeDto> GetPatientDischargeWithEncounterId(long encounterId)
        {
            var patientEncounter = await _encounterCreateHandler.Handle(encounterId);

            var discharge = await _baseQueryHandler.GetPatientDischargeWithEncounterId(patientEncounter.PatientId, patientEncounter.Id);

            return discharge;
        }
        public async Task<DischargeDto> GetPatientDischarge(long dischargeId)
        {
            if(dischargeId == 0)
                throw new UserFriendlyException("Discharge Id is required.");

            return await _baseQueryHandler.GetDischargeInformation(dischargeId);
        }
    }
}
