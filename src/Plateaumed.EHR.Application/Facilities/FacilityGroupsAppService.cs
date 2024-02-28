using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Organizations.Abstractions;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff.Abstractions;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_FacilityGroups)]
    public class FacilityGroupsAppService : EHRAppServiceBase, IFacilityGroupsAppService
    {
        private readonly IRepository<FacilityGroup, long> _facilityGroupRepository;
        private readonly IRepository<FacilityType, long> _facilityTypeRepository;
        private readonly ICreateDefaultOrganizationUnitsCommandHandler _createDefaultOrganizationUnits;
        private readonly ICreateDefaultJobHierarchyCommandHandler _createDefaultJobHierarchy;
        private readonly ICreateDefaultBedTypesCommandHandler _createDefaultBedTypes;

        public FacilityGroupsAppService(
            IRepository<FacilityGroup, long> facilityGroupRepository, 
            IRepository<FacilityType, long> facilityTypeRepository, 
            ICreateDefaultOrganizationUnitsCommandHandler createDefaultOrganizationUnits,
            ICreateDefaultJobHierarchyCommandHandler createDefaultJobHierarchy,
            ICreateDefaultBedTypesCommandHandler createDefaultBedTypes)
        {
            _createDefaultJobHierarchy = createDefaultJobHierarchy;
            _createDefaultBedTypes = createDefaultBedTypes;
            _facilityGroupRepository = facilityGroupRepository;
            _facilityTypeRepository = facilityTypeRepository;
            _createDefaultOrganizationUnits = createDefaultOrganizationUnits;
        }

        public async Task<PagedResultDto<GetFacilityGroupForViewDto>> GetAll(
            GetAllFacilityGroupsInput input
        )
        {
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Split(" ") : null;

            var filteredFacilityGroups = _facilityGroupRepository
                .GetAll()
                .Include(f => f.ChildFacilities)
                .ThenInclude(f => f.TypeFk)
                .WhereIf(
                    filterTerms != null,
                    f => filterTerms.All(term =>
                        f.Name.ToLower().Contains(term)
                        || f.City.ToLower().Contains(term)
                        || f.State.ToLower().Contains(term)
                        || f.PostCode.ToLower().Contains(term)
                    )
                );

            var pagedAndFilteredFacilityGroups = filteredFacilityGroups
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var totalCount = await filteredFacilityGroups.CountAsync();

            var facilityGroups = await pagedAndFilteredFacilityGroups.ToListAsync();

            var results = facilityGroups.Select((fg) =>
            {
                var fg1 = new GetFacilityGroupForViewDto
                {
                    FacilityGroup = ObjectMapper.Map<FacilityGroupDto>(fg)
                };
                var result = GetTenantCatgory(fg.TenantId);
                fg1.FacilityGroup.Category = GetTenantCatgory(fg.TenantId);

                return fg1;
            }
            ).ToList();


            return new PagedResultDto<GetFacilityGroupForViewDto>(totalCount, results);
        }

        public async Task<GetFacilityGroupForEditOutput> GetFacilityGroupForEdit(
            EntityDto<long> input
        )
        {
            FacilityGroup facilityGroup = await _facilityGroupRepository
                    .GetAll()
                    .Include(f => f.ChildFacilities)
                    .ThenInclude(f => f.TypeFk)
                    .Where(f => f.Id == input.Id)
                    .FirstOrDefaultAsync();

            var output = new GetFacilityGroupForEditOutput
            {
                FacilityGroup = ObjectMapper.Map<CreateOrEditFacilityGroupDto>(facilityGroup)
            };
            output.FacilityGroup.Category = GetTenantCatgory(facilityGroup.TenantId);

            return output;
        }

        public async Task<GetFacilityGroupForEditOutput> GetFacilityGroupDetails()
        {
            var facilityGroup = await _facilityGroupRepository
                .GetAll()
                .Include(f => f.ChildFacilities)
                .ThenInclude(f => f.TypeFk)
                .FirstOrDefaultAsync();

            if (AbpSession.TenantId != null) { facilityGroup.TenantId = (int)AbpSession.TenantId; }

            var output = new GetFacilityGroupForEditOutput
            {
                FacilityGroup = ObjectMapper.Map<CreateOrEditFacilityGroupDto>(facilityGroup)
            };
            output.FacilityGroup.Category = GetTenantCatgory(facilityGroup.TenantId);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityGroups_Edit)]
        public async Task<GetFacilityGroupBankDetailsForEditOutput> GetFacilityGroupBankDetails()
        {
            var facilityGroup = await _facilityGroupRepository
                .GetAll()
                .Include(f => f.ChildFacilities)
                .ThenInclude(b => b.FacilityBanks)
                .FirstOrDefaultAsync();

            var output = new GetFacilityGroupBankDetailsForEditOutput
            {
                FacilityGroup = ObjectMapper.Map<CreateOrEditFacilityGroupBankRequest>(facilityGroup)
            };

            return output;
        }

        public async Task<GetFacilityGroupPatientDetailsForEditOutput> GetFacilityGroupPatientCodeTemplateDetails()
        {
            var facilityGroup = await _facilityGroupRepository
                .GetAll()
                .Include(f => f.ChildFacilities)
                .ThenInclude(pc => pc.PatientCodeTemplate)
                .FirstOrDefaultAsync();

            var facilityGroupDto = ObjectMapper.Map<CreateOrEditFacilityGroupPatientCodeTemplateDto>(facilityGroup);

            var output = new GetFacilityGroupPatientDetailsForEditOutput
            {
                FacilityGroup = facilityGroupDto
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditFacilityGroupDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        public async Task CreateOrEditBankDetails(CreateOrEditFacilityGroupBankRequest input)
        {
            if (input.Id != null)
            {
                await UpdateBankDetails(input);
            }
        }

        public async Task CreateOrEditPatientCodeTemplateDetails(CreateOrEditFacilityGroupPatientCodeTemplateDto input)
        {
            if (input.Id != null)
            {
                await CreateOrEditPatientCodeTemplateAndOtherDetails(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityGroups_Create)]
        protected virtual async Task Create(CreateOrEditFacilityGroupDto input)
        {
            var facilityGroup = ObjectMapper.Map<FacilityGroup>(input);

            if (AbpSession.TenantId != null)
            {
                facilityGroup.TenantId = (int)AbpSession.TenantId;
            }

            var saved = await _facilityGroupRepository.InsertAsync(facilityGroup);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            
            foreach (var facility in saved.ChildFacilities)
            {
                if (AbpSession.TenantId != null)
                {
                    facility.TenantId = (int)AbpSession.TenantId;
                }

                var facilityType = await _facilityTypeRepository.GetAll()
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(f => f.Id == facility.TypeId);

                if (facilityType.Name == "Hospital")
                {
                    await _createDefaultOrganizationUnits.Handle(facility.TenantId, facility.Id);
                    await _createDefaultJobHierarchy.Handle(facility.TenantId, facility.Id);
                    await _createDefaultBedTypes.Handle(facility.TenantId, facility.Id);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityGroups_Edit)]
        protected virtual async Task Update(CreateOrEditFacilityGroupDto input)
        {
            var facilityGroup = await _facilityGroupRepository.FirstOrDefaultAsync((long)input.Id);

            ObjectMapper.Map(input, facilityGroup);

            var newHospitals = new List<Facility>();
            
            foreach (var facility in facilityGroup.ChildFacilities)
            {
                if (AbpSession.TenantId != null)
                {
                    facility.TenantId = (int)AbpSession.TenantId;
                }

                var facilityType = await _facilityTypeRepository.GetAll()
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(f => f.Id == facility.TypeId);

                if (facilityType.Name == "Hospital" && facility.Id == 0)
                {
                    newHospitals.Add(facility);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            foreach (var facility in newHospitals)
            {
                await _createDefaultOrganizationUnits.Handle(facility.TenantId, facility.Id);
                await _createDefaultJobHierarchy.Handle(facility.TenantId, facility.Id);
                await _createDefaultBedTypes.Handle(facility.TenantId, facility.Id);
            }
        }

         [AbpAuthorize(AppPermissions.Pages_FacilityGroups_Edit)]
        protected virtual async Task UpdateBankDetails(CreateOrEditFacilityGroupBankRequest input)
        {
            var facilityGroup = await _facilityGroupRepository
                .GetAll()
                .Include(f => f.ChildFacilities)
                .Where(f => f.Id == input.Id)
                .FirstOrDefaultAsync();

            var facilityGroupDto = ObjectMapper.Map<CreateOrEditFacilityGroupBankRequest>(facilityGroup);

            var childFacilityInput = input.ChildFacilities ?? new List<CreateOrEditBankRequest>();

            foreach (var facility in facilityGroup.ChildFacilities)
            {
                var match = childFacilityInput.FirstOrDefault(i => facility.Id == i.Id);

                if (match != null)
                {
                    facility.BankName = match.BankName;
                    facility.BankAccountHolder = match.BankAccountHolder;
                    facility.BankAccountNumber = match.BankAccountNumber;
                    facility.IsActive = match.IsActive;
                    facility.isDefault = match.IsDefault;
                }
            }

            await _facilityGroupRepository.UpdateAsync(facilityGroup);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityGroups_Edit)]
        protected virtual async Task CreateOrEditPatientCodeTemplateAndOtherDetails(CreateOrEditFacilityGroupPatientCodeTemplateDto input)
        {
            var facilityGroup = await _facilityGroupRepository
                .GetAll()
                .Include(f => f.ChildFacilities)
                .Where(f => f.Id == input.Id)
                .FirstOrDefaultAsync();

            var childFacilityInput = input.ChildFacilities ?? new List<CreateOrEditFacilityPatientCodeTemplateDto>();

            foreach (var facility in facilityGroup.ChildFacilities)
            {
                var match = childFacilityInput.FirstOrDefault(i => facility.Id == i.Id);

                if (match != null)
                {
                    facility.HasLaboratory = match.HasLaboratory;
                    facility.HasPharmacy = match.HasPharmacy;
                    facility.PatientCodeTemplate = ObjectMapper.Map<PatientCodeTemplate>(match.PatientCodeTemplate);
                }
            }

            await _facilityGroupRepository.UpdateAsync(facilityGroup);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityGroups_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _facilityGroupRepository.DeleteAsync(input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private MultiTenancy.TenantCategoryType GetTenantCatgory(int tenantId)
        {
            return TenantManager.Tenants
                .FirstOrDefault(t => t.Id == tenantId).Category;
        }
    }
}
