using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Organizations.Abstractions;
using Plateaumed.EHR.Staff.Abstractions;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_Facilities)]
    public class FacilitiesAppService : EHRAppServiceBase, IFacilitiesAppService
    {
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IRepository<FacilityGroup, long> _lookup_facilityGroupRepository;
        private readonly IRepository<FacilityType, long> _lookup_facilityTypeRepository;
        private readonly ICreateDefaultOrganizationUnitsCommandHandler _createDefaultOrganizationUnits;
        private readonly ICreateDefaultJobHierarchyCommandHandler _createDefaultJobHierarchy;
        private readonly ICreateDefaultBedTypesCommandHandler _createDefaultBedTypes;
        private readonly IActivateOrDeactivatePharmacyCommandHandler _activatePharmacy;
        private readonly IActivateOrDeactivateLaboratoryCommandHandler _activateLaboratory;
        private readonly IGetUserFacilityQueryHandler _getUserFacilityQueryHandler;

        public FacilitiesAppService(
            IRepository<Facility, long> facilityRepository,
            IRepository<FacilityGroup, long> lookup_facilityGroupRepository,
            IRepository<FacilityType, long> lookup_facilityTypeRepository,
            ICreateDefaultOrganizationUnitsCommandHandler createDefaultOrganizationUnits,
            ICreateDefaultJobHierarchyCommandHandler createDefaultJobHierarchy,
            ICreateDefaultBedTypesCommandHandler createDefaultBedTypes,
            IActivateOrDeactivatePharmacyCommandHandler activatePharmacy,
            IActivateOrDeactivateLaboratoryCommandHandler activateLaboratory,
            IGetUserFacilityQueryHandler getUserFacilityQueryHandler
            )
        {
            _createDefaultJobHierarchy = createDefaultJobHierarchy;
            _createDefaultBedTypes = createDefaultBedTypes;
            _facilityRepository = facilityRepository;
            _lookup_facilityGroupRepository = lookup_facilityGroupRepository;
            _lookup_facilityTypeRepository = lookup_facilityTypeRepository;
            _createDefaultOrganizationUnits = createDefaultOrganizationUnits;
            _activatePharmacy = activatePharmacy;
            _activateLaboratory = activateLaboratory;
            _getUserFacilityQueryHandler = getUserFacilityQueryHandler;
        }

        public async Task<PagedResultDto<GetFacilityForViewDto>> GetAll(GetAllFacilitiesInput input)
        {
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Split(" ") : null;

            var filteredFacilities = _facilityRepository
                .GetAll()
                .Include(f => f.GroupFk)
                .Include(f => f.TypeFk)
                .WhereIf(
                    filterTerms != null,
                    f => filterTerms.All(term =>
                        f.Name.ToLower().Contains(term)
                        || f.City.ToLower().Contains(term)
                        || f.State.ToLower().Contains(term)
                        || f.PostCode.ToLower().Contains(term)
                    )
                )
                .WhereIf(
                    input.FacilityGroupIdFilter.HasValue,
                    e => e.GroupId == input.FacilityGroupIdFilter
                )
                .WhereIf(
                    input.FacilityTypeIdFilter.HasValue,
                    e => e.TypeId == input.FacilityTypeIdFilter
                );

            var pagedAndFilteredFacilities = filteredFacilities
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var totalCount = await filteredFacilities.CountAsync();

            var facilities = await pagedAndFilteredFacilities.ToListAsync();

            var results = facilities.Select(facility => new GetFacilityForViewDto
            {
                Facility = ObjectMapper.Map<FacilityDto>(facility)
            }).ToList();

            return new PagedResultDto<GetFacilityForViewDto>(totalCount, results);
        }

       [AbpAuthorize(AppPermissions.Pages_Facilities_Edit)]
        public async Task<GetFacilityForEditOutput> GetFacilityForEdit(EntityDto<long> input)
        {
            var facility = await _facilityRepository.GetAll()
                .Include(f => f.GroupFk)
                .Include(f => f.TypeFk)
                .Include(f => f.PatientCodeTemplate)
                .Include(f => f.StaffCodeTemplateFk)
                .Where(f => f.Id == input.Id)
                .FirstOrDefaultAsync();

            var output = new GetFacilityForEditOutput
            {
                Facility = ObjectMapper.Map<CreateOrEditFacilityDto>(facility),
                FacilityGroup = facility.GroupFk.Name,
                FacilityType = facility.TypeFk.Name
            };

            return output;
        }

        public async Task CreateOrEditMultipleFacilities(List<CreateOrEditFacilityDto> input)
        {
            foreach (CreateOrEditFacilityDto facility in input)
            {
                await CreateOrEdit(facility);
            }
        }

        public async Task CreateOrEdit(CreateOrEditFacilityDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

       [AbpAuthorize(AppPermissions.Pages_Facilities_Create)]
        protected virtual async Task Create(CreateOrEditFacilityDto input)
        {
            if (input.GroupId == 0)
            {
                throw new UserFriendlyException("Facility Group Id cannot be zero, \"0\"");
            }

            var facility = ObjectMapper.Map<Facility>(input);
            var tenantId = (int)AbpSession.TenantId;

            if (AbpSession.TenantId != null)
            {
                facility.TenantId = tenantId;
            }

            await _facilityRepository.InsertAsync(facility);
            await CurrentUnitOfWork.SaveChangesAsync();

            var facilityType = await _lookup_facilityTypeRepository.GetAll()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(f => f.Id == facility.TypeId);

            if (facilityType.Name == "Hospital")
            {
                await _createDefaultOrganizationUnits.Handle(tenantId, facility.Id);
                await _createDefaultJobHierarchy.Handle(tenantId, facility.Id);
                await _createDefaultBedTypes.Handle(tenantId, facility.Id);
            }
        }

        //TODO: This runs when the input.Id value = 0. Fix!
        [AbpAuthorize(AppPermissions.Pages_Facilities_Edit)]
        protected virtual async Task Update(CreateOrEditFacilityDto input)
        {
            var facility = await _facilityRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, facility);

            await CurrentUnitOfWork.SaveChangesAsync();
        }



        [AbpAuthorize(AppPermissions.Pages_Facilities_Edit)]
        public virtual async Task ActivateorDeactivateFacility(ActivateOrDeactivateFacility input)
        {
            var facility = await _facilityRepository.FirstOrDefaultAsync((long)input.Id);
            if (facility == null)
            {
                throw new UserFriendlyException("Facility cannot be found");
            }
            facility.IsActive = input.IsActive;

            await _facilityRepository.UpdateAsync(facility);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Facilities_Edit)]
        public virtual async Task ActivateOrDeactivatePharmacy(ActivateOrDeactivatePharmacyRequest input)
        {
            await _activatePharmacy.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Facilities_Edit)]
        public virtual async Task ActivateOrDeactivateLaboratory(ActivateOrDeactivateLaboratoryRequest input)
        {
            await _activateLaboratory.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Facilities_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _facilityRepository.DeleteAsync(input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Facilities)]
        public async Task<
            PagedResultDto<FacilityFacilityGroupLookupTableDto>
        > GetAllFacilityGroupForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_facilityGroupRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.ToLower().Contains(input.Filter.ToLower())
                );

            var totalCount = await query.CountAsync();

            var facilityGroupList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<FacilityFacilityGroupLookupTableDto>();
            foreach (var facilityGroup in facilityGroupList)
            {
                lookupTableDtoList.Add(
                    new FacilityFacilityGroupLookupTableDto
                    {
                        Id = facilityGroup.Id,
                        DisplayName = facilityGroup.Name
                    }
                );
            }

            return new PagedResultDto<FacilityFacilityGroupLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Facilities)]
        public async Task<
            PagedResultDto<FacilityFacilityTypeLookupTableDto>
        > GetAllFacilityTypeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_facilityTypeRepository
                .GetAll()
                .IgnoreQueryFilters()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.ToLower().Contains(input.Filter.ToLower())
                );

            var totalCount = await query.CountAsync();

            var facilityTypeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<FacilityFacilityTypeLookupTableDto>();
            foreach (var facilityType in facilityTypeList)
            {
                lookupTableDtoList.Add(
                    new FacilityFacilityTypeLookupTableDto
                    {
                        Id = facilityType.Id,
                        DisplayName = facilityType.Name
                    }
                );
            }

            return new PagedResultDto<FacilityFacilityTypeLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task<List<GetUserFacilityViewDto>> GetUserFacility(string email)
        {
            return await _getUserFacilityQueryHandler.Handle(email);
        }
    }
}
