using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Staff.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.Organizations.Dtos;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Onboarding.Dto
{
    public class OnboardingReviewsDto : EntityDto<long>
    {
        public GetFacilityGroupForEditOutput Facilities { get; set; }

        public GetFacilityGroupBankDetailsForEditOutput Banks { get; set; }

        public List<GetFacilityInsurersForEditOutput> Insurers { get; set; }

        public List<PagedResultDto<GetFacilityDocumentForViewDto>> Documents { get; set; }

        public GetFacilityGroupPatientDetailsForEditOutput AdditionalDetails { get; set; }

        public List<WardDto> Wards { get; set; }

        public List<OrganizationUnitDto> OrganizationUnits { get; set; }

        public List<ClinicListDto> Clinics { get; set; }

        public PagedResultDto<JobTitleDto> JobTitles { get; set; }
    }
}