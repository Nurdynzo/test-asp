using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.PatientAppointments.Dtos;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients
{
    public interface IPatientAppointmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPatientAppointmentForViewDto>> GetAll(GetAllPatientAppointmentsInput input);

        Task<GetPatientAppointmentForEditOutput> GetPatientAppointmentForEdit(EntityDto<long> input);

        Task<PagedResultDto<AppointmentListResponse>> GetAppointmentsByPatientId(GetAppointmentsByPatientIdRequest request);

        Task UpdateAppointmentStatus(EditAppointmentStatusDto input);

        Task<PagedResultDto<PatientAppointmentPatientLookupTableDto>> GetAllPatientForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<PatientAppointmentPatientReferralLookupTableDto>> GetAllPatientReferralForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<PatientAppointmentStaffMemberLookupTableDto>> GetAllStaffMemberForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<PatientAppointmentOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForLookupTable(GetAllForLookupTableInput input);

    }
}