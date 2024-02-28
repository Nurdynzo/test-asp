using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using System.Threading.Tasks;
using Plateaumed.EHR.ReferAndConsults.Abstraction;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using System.Collections.Generic;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.ReferAndConsults
{
    [AbpAuthorize(AppPermissions.Pages_ReferConsult)]
    public class ReferAndConsultAppService : EHRAppServiceBase, IReferAndConsultAppService
    {
        private readonly ICreateReferralOrConsultLetterCommandHandler _createIReferralOrConsultLetterCommandHandler;
        private readonly IEditReferralOrConsultLetterCommandHandler _editReferralOrConsultLetterCommandHandler;
        private readonly IGetConsultationLetterQueryHandler _getConsultationLetterQueryHandler;
        private readonly IGetReferralLetterQueryHandler _getReferralLetterQueryHandler;
        private readonly IGetHospitalListQueryHandler _getHospitalsQueryHandler;
        private readonly IGetHospitalUnitQueryHandler _getHospitalUnitQueryHandler;
        private readonly IGetHospitalConsultantQueryHandler _getConsultantsQueryHandler;
        

        public ReferAndConsultAppService(ICreateReferralOrConsultLetterCommandHandler createIReferralOrConsultLetterCommandHandler,
            IEditReferralOrConsultLetterCommandHandler editReferralOrConsultLetterCommandHandler,
            IGetConsultationLetterQueryHandler getConsultationLetterQueryHandler,
            IGetReferralLetterQueryHandler getReferralLetterQueryHandler,
            IGetHospitalListQueryHandler getHospitalsQueryHandler,
            IGetHospitalUnitQueryHandler getHospitalUnitQueryHandler,
            IGetHospitalConsultantQueryHandler getConsultantsQueryHandler)
        {
            _createIReferralOrConsultLetterCommandHandler = createIReferralOrConsultLetterCommandHandler;
            _editReferralOrConsultLetterCommandHandler = editReferralOrConsultLetterCommandHandler;
            _getConsultationLetterQueryHandler = getConsultationLetterQueryHandler;
            _getReferralLetterQueryHandler = getReferralLetterQueryHandler;
            _getHospitalsQueryHandler = getHospitalsQueryHandler;
            _getHospitalUnitQueryHandler = getHospitalUnitQueryHandler;
            _getConsultantsQueryHandler = getConsultantsQueryHandler;
        }
        public async Task<List<HospitalDto>> GetHospitals() =>
            await _getHospitalsQueryHandler.Handle();

        public async Task<List<OrganizationUnitDto>> GetHospitalUnits(long facilityId) =>
            await _getHospitalUnitQueryHandler.Handle(facilityId);

        public async Task<List<FacilityStaffDto>> GetHospitalConsultants(long facilityId) =>
            await _getConsultantsQueryHandler.Handle(facilityId);

        [AbpAuthorize(AppPermissions.Pages_ReferConsult_Create)]
        public async Task<CreateReferralOrConsultLetterDto> SaveReferralOrConsultLetter(CreateReferralOrConsultLetterDto input)
        {
            return input.Id > 0
                ? await _editReferralOrConsultLetterCommandHandler.Handle(input)
                : await _createIReferralOrConsultLetterCommandHandler.Handle(input);
        }

        public async Task<ConsultReturnDto> GetConsultationLetter(ConsultRequestDto request)
        {
            var staffUser = await GetCurrentUserAsync();
            return await _getConsultationLetterQueryHandler.Handle(request, staffUser);
        }

        public async Task<ReferralReturnDto> GetReferralLetter(ReferralRequestDto request)
        {
            var staffUser = await GetCurrentUserAsync();
            return await _getReferralLetterQueryHandler.Handle(request,staffUser);
        }
    }
}
