using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients
{
    public interface IPatientsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPatientForViewDto>> GetAll(GetAllPatientsInput input);

        Task<List<CheckPatientExistOutput>> CheckPatientExist(CheckPatientExistInput input);

        Task<CreateOrEditPatientDto> GetPatientForEdit(EntityDto<long> input);

        Task<CreateOrEditPatientDto>  CreateOrEdit(CreateOrEditPatientDto input);
        
        Task Delete(EntityDto<long> input);

        Task<PagedResultDto<PatientPatientOccupationLookupTableDto>> GetAllPatientOccupationForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<GetPatientLandingListOuptDto>> GetOutPatientLandingList(GetAllForLookupTableInput input);

        Task<string> UpdateAppointmentStatusFromAwaitingVitals(long encounterId);

        List<string> GetIntensityUnits();

        /// <summary>
        /// Generate new patient code using current facility template setting
        /// </summary>
        /// <returns></returns>
        Task<string> GetNewPatientCode();
        
        /// <summary>
        /// Search patient by fullname, code, phone number, email, gender etc
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        Task<List<SearchPatientOutput>> SearchPatient(string searchText);

        /// <summary>
        /// Get patient details using the patient id
        /// </summary>
        /// <param name="GetPatientDetails"></param>
        /// <returns></returns>
        Task<GetPatientDetailsOutDto> GetPatientDetails(GetPatientDetailsInput input);

        Task<PagedResultDto<GetInpatientLandingListResponse>> GetInpatientLandingList(
            GetInpatientLandingListRequest request);

        Task<GetPatientsMedicationsResponse> GetPatientMedications(long patientId);

        Task<PagedResultDto<GetAccidentAndEmergencyLandingListResponse>> GetAccidentAndEmergencyLandingList(
            GetAccidentAndEmergencyLandingListRequest request);

        Task CreatePatientStabilityStatus(PatientStabilityRequestDto request);

        Task<List<PatientStabilityResponseDto>> GetPatientStabilityStatus(long patientId, long encounterId);

        List<string> GetStabilityStatus();
    }
}
