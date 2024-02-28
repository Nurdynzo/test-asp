using Abp.Authorization;
using Abp.UI;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.NextAppointments
{
    [AbpAuthorize(AppPermissions.Pages_NextAppointment)]
    public class NextAppointmentAppService : EHRAppServiceBase, INextAppointmentAppService
    {
        private readonly ICreateNextAppointmentCommandHandler _createNextAppointmentCommandHandler;
        private readonly IEditNextAppointmentCommandHandler _editNextAppointmentCommandHandler;
        private readonly IGetDoctorAllNextAppointmentsQueryHandler _doctorNextAppointmentCommandHandler;
        private readonly IGetDoctorAndPatientAppointmentQueryHandler _doctorPatientNextAppointmentCommandHandler;
        private readonly IGetUnitsOrClinicsQueryHandler _getUnitQueryHandler;
        private readonly IGetPatientAllNextAppointmentQueryHandler _getPatientNextAppointmentQueryHandler;

        public NextAppointmentAppService(
            ICreateNextAppointmentCommandHandler createNextAppointmentCommandHandler,
            IEditNextAppointmentCommandHandler editNextAppointmentCommandHandler,
            IGetDoctorAllNextAppointmentsQueryHandler doctorNextAppointmentCommandHandler,
            IGetDoctorAndPatientAppointmentQueryHandler doctorPatientNextAppointmentCommandHandler,
            IGetUnitsOrClinicsQueryHandler getUnitQueryHandler,
            IGetPatientAllNextAppointmentQueryHandler getPatientNextAppointmentQueryHandler
            )
        {
            _createNextAppointmentCommandHandler = createNextAppointmentCommandHandler;
            _editNextAppointmentCommandHandler = editNextAppointmentCommandHandler;
            _doctorNextAppointmentCommandHandler = doctorNextAppointmentCommandHandler;
            _doctorPatientNextAppointmentCommandHandler = doctorPatientNextAppointmentCommandHandler;
            _getUnitQueryHandler = getUnitQueryHandler;
            _getPatientNextAppointmentQueryHandler = getPatientNextAppointmentQueryHandler;
        }

        [AbpAuthorize(AppPermissions.Pages_NextAppointment_Create)]
        public async Task<CreateNextAppointmentDto> CreateOrEdit(CreateNextAppointmentDto input)
        {
            if (input == null)
                throw new UserFriendlyException("Request data is required.");

            CreateNextAppointmentDto returnResult;
            var staffMemberUser = await GetCurrentUserAsync();
            if (input.Id > 0)
                returnResult = await _editNextAppointmentCommandHandler.Handle(input, staffMemberUser.Id);
            else
                returnResult = await _createNextAppointmentCommandHandler.Handle(input, staffMemberUser.Id);

            return returnResult;
        }

        public async Task<List<NextAppointmentReturnDto>> GetPatientNextAppointments(int patientId)
        {
            return await _getPatientNextAppointmentQueryHandler.Handle(patientId);
        }

        public async Task<List<NextAppointmentUnitReturnDto>> GetAvailableUnitAndClinics(long encounterId)
        {
            var staffMemberUser = await GetCurrentUserAsync();
            var facilityId = GetCurrentUserFacilityId();
            return await _getUnitQueryHandler.Handle(staffMemberUser.Id, facilityId, encounterId);
        }

        public async Task<List<NextAppointmentReturnDto>> GetDoctorNextAppointments()
        {
            var staffMemberUser = await GetCurrentUserAsync();
            return await _doctorNextAppointmentCommandHandler.Handle(staffMemberUser.Id);
        }

        public async Task<List<NextAppointmentReturnDto>> GetDoctorPatientNextAppointments(long patientId)
        {
            var staffMemberUser = await GetCurrentUserAsync();
            return await _doctorPatientNextAppointmentCommandHandler.Handle(patientId, staffMemberUser.Id);
        }
    }
}
