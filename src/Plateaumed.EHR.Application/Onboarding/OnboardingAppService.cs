using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Localization;
using Plateaumed.EHR.Onboarding.Dto;
using Plateaumed.EHR.Staff.Dtos;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Abp.UI;

namespace Plateaumed.EHR.Onboarding
{
    [AbpAuthorize(AppPermissions.Pages_FacilityGroups)]
    public class OnboardingReviewsAppService : EHRAppServiceBase, IOnboardingReviewsAppService
    {
        private readonly IRepository<FacilityGroup, long> _facilityGroupRepository;
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IFacilityGroupsAppService _facilityGroupsAppService;
        private readonly IWardsAppService _wardsAppService;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly IJobTitlesAppService _jobTitlesAppService;
        private readonly IFacilityInsurersAppService _facilityInsurersAppService;
        private readonly IFacilityDocumentsAppService _facilityDocumentsAppService;

        public OnboardingReviewsAppService(
            IRepository<Facility, long> facilityRepository,
            IRepository<FacilityGroup, long> facilityGroupRepository,
            IFacilityGroupsAppService facilityGroupsAppService,
            IWardsAppService wardsAppService,
            IOrganizationUnitAppService organizationUnitAppService,
            IJobTitlesAppService jobTitlesAppService,
            IFacilityInsurersAppService facilityInsurersAppService,
            IFacilityDocumentsAppService facilityDocumentsAppService)
        {
            _facilityRepository = facilityRepository;
            _facilityGroupRepository = facilityGroupRepository;
            _facilityGroupsAppService = facilityGroupsAppService;
            _wardsAppService = wardsAppService;
            _organizationUnitAppService = organizationUnitAppService;
            _jobTitlesAppService = jobTitlesAppService;
            _facilityInsurersAppService = facilityInsurersAppService;
            _facilityDocumentsAppService = facilityDocumentsAppService;
        }

        public async Task<OnboardingReviewsDto> GetReviewDetails(GetAllFacilitiesInput input)
        {
            var jobTitlesInputDto = new GetAllJobTitlesInput();
            jobTitlesInputDto.IncludeLevels = true;

            var facilityDetails = await _facilityGroupsAppService.GetFacilityGroupDetails();
            var additionalDetails = await _facilityGroupsAppService.GetFacilityGroupPatientCodeTemplateDetails();
            var wards = await _wardsAppService.GetAllWards();
            var organizationUnits = await _organizationUnitAppService.GetAll();
            var clinics = await _organizationUnitAppService.GetClinics();
            var jobTitles = await _jobTitlesAppService.GetAll(jobTitlesInputDto);
            var facilityBanks = await _facilityGroupsAppService.GetFacilityGroupBankDetails();



            var group = await _facilityGroupRepository
                .GetAll()
                .Include(f => f.ChildFacilities)
                .FirstOrDefaultAsync();

            var facilityDocuments = new List<PagedResultDto<GetFacilityDocumentForViewDto>>();
            var facilityInsurers = new List<GetFacilityInsurersForEditOutput>();

            foreach (var item in group.ChildFacilities)
            {
                var documentInput = new GetAllFacilityDocumentsInput
                {
                    FacilityIdFilter = input.FacilityIdFilter
                };

                var insurers = await _facilityInsurersAppService
                    .GetFacilityInsurersForEdit(new EntityDto<long>(item.Id));
                var documents = await _facilityDocumentsAppService.GetAll(documentInput);

                facilityInsurers.Add(insurers);
                facilityDocuments.Add(documents);
            }

            var reviewDetails = new OnboardingReviewsDto
            {
                Facilities = facilityDetails,
                Banks = facilityBanks,
                Insurers = facilityInsurers,
                Documents = facilityDocuments,
                AdditionalDetails = additionalDetails,
                Wards = wards,
                OrganizationUnits = organizationUnits,
                Clinics = clinics,
                JobTitles = jobTitles
            };

            return reviewDetails;
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityGroups_Edit)]
        public async Task<GetFacilityBankInsuranceKYCDetails> GetFacilityBankInsuranceKYCDetailsByFacility(EntityDto<long> input)
        {
            if (input?.Id == 0) { throw new UserFriendlyException(L("FacilityIdCannotBeNull")); }

            var facility = await _facilityRepository.GetAll().FirstOrDefaultAsync(i => i.Id == input.Id);

            if (facility != null)
            {
                var insurers = await _facilityInsurersAppService.GetFacilityInsurersForEdit(new EntityDto<long>(input.Id));
                var kycDocs = await _facilityDocumentsAppService.GetAllFacilityDocumentForView(new EntityDto<long>(input.Id));

                var result = new GetFacilityBankInsuranceKYCDetails()
                {
                    Name = facility.Name,
                    BankName = facility.BankName,
                    BankAccountHolder = facility.BankAccountHolder,
                    BankAccountNumber = facility.BankAccountNumber,
                    Insurers = insurers.FacilityInsurers,
                    KYCDocuments = kycDocs
                };

                return result;
            }
            return null;
        }
    }
}
