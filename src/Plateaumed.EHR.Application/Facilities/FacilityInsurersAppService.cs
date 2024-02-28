using Plateaumed.EHR.Insurance;
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
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;

namespace Plateaumed.EHR.Facilities
{
   [AbpAuthorize(AppPermissions.Pages_FacilityInsurers)]
    public class FacilityInsurersAppService : EHRAppServiceBase, IFacilityInsurersAppService
    {
        private readonly IRepository<FacilityInsurer, long> _facilityInsurerRepository;
        private readonly IRepository<FacilityGroup, long> _lookup_facilityGroupRepository;
        private readonly IRepository<Facility, long> _lookup_facilityRepository;
        private readonly IRepository<InsuranceProvider, long> _lookup_insuranceProviderRepository;
        private readonly IActivateOrDeactivateFacilityInsurerCommandHandler _activateFacilityInsurer;

        public FacilityInsurersAppService(
            IRepository<FacilityInsurer, long> facilityInsurerRepository,
            IRepository<FacilityGroup, long> lookup_facilityGroupRepository,
            IRepository<Facility, long> lookup_facilityRepository,
            IRepository<InsuranceProvider, long> lookup_insuranceProviderRepository,
            IActivateOrDeactivateFacilityInsurerCommandHandler activateFacilityInsurer
        )
        {
            _facilityInsurerRepository = facilityInsurerRepository;
            _lookup_facilityGroupRepository = lookup_facilityGroupRepository;
            _lookup_facilityRepository = lookup_facilityRepository;
            _lookup_insuranceProviderRepository = lookup_insuranceProviderRepository;
            _activateFacilityInsurer = activateFacilityInsurer;
        }

        public async Task<PagedResultDto<GetFacilityInsurerForViewDto>> GetAll(
            GetAllFacilityInsurersInput input
        )
        {
            var filteredFacilityInsurers = _facilityInsurerRepository
                .GetAll()
                .Include(e => e.FacilityGroupFk)
                .Include(e => e.FacilityFk)
                .Include(e => e.InsuranceProviderFk)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                .WhereIf(
                    input.IsActiveFilter.HasValue && input.IsActiveFilter > -1,
                    e =>
                        (input.IsActiveFilter == 1 && e.IsActive)
                        || (input.IsActiveFilter == 0 && !e.IsActive)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.FacilityGroupNameFilter),
                    e =>
                        e.FacilityGroupFk != null
                        && e.FacilityGroupFk.Name == input.FacilityGroupNameFilter
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.FacilityNameFilter),
                    e => e.FacilityFk != null && e.FacilityFk.Name == input.FacilityNameFilter
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.InsuranceProviderNameFilter),
                    e =>
                        e.InsuranceProviderFk != null
                        && e.InsuranceProviderFk.Name == input.InsuranceProviderNameFilter
                );

            var pagedAndFilteredFacilityInsurers = filteredFacilityInsurers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var facilityInsurers =
                from o in pagedAndFilteredFacilityInsurers
                join o1 in _lookup_facilityGroupRepository.GetAll()
                    on o.FacilityGroupId equals o1.Id
                    into j1
                from s1 in j1.DefaultIfEmpty()
                join o2 in _lookup_facilityRepository.GetAll() on o.FacilityId equals o2.Id into j2
                from s2 in j2.DefaultIfEmpty()
                join o3 in _lookup_insuranceProviderRepository.GetAll() on o.InsuranceProviderId equals o3.Id into j3
                from s3 in j3.DefaultIfEmpty()
                select new
                {
                    o.IsActive,
                    o.Id,
                    o.FacilityGroupId,
                    o.FacilityId,
                    FacilityGroupName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                    FacilityName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                    InsuranceProviderName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                };

            var totalCount = await filteredFacilityInsurers.CountAsync();

            var dbList = await facilityInsurers.ToListAsync();
            var results = new List<GetFacilityInsurerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetFacilityInsurerForViewDto()
                {
                    FacilityInsurer = new FacilityInsurerDto {
                        IsActive = o.IsActive,
                        FacilityGroupId = o.FacilityGroupId,
                        FacilityId = o.FacilityId,
                        Id = o.Id, },
                    FacilityGroupName = o.FacilityGroupName,
                    FacilityName = o.FacilityName,
                    InsuranceProviderName = o.InsuranceProviderName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetFacilityInsurerForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityInsurers_Edit)]
        public async Task<GetFacilityInsurersForEditOutput> GetFacilityInsurersForEdit(
            EntityDto<long> input
        )
        {
            var facilityInsurers = _facilityInsurerRepository
                .GetAll()
                .Include(i => i.FacilityFk)
                .Include(i => i.FacilityGroupFk)
                .Include(i => i.InsuranceProviderFk)
                .IgnoreQueryFilters()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input?.Id.ToString()),
                    e => e.FacilityFk != null && e.FacilityFk.Id == input.Id && e.IsDeleted == false)
                .OrderBy("id desc");

            var facilityInsurersList = await facilityInsurers.ToListAsync();
            var insurers = new List<CreateOrEditFacilityInsurerDto>();

            if (facilityInsurersList.Count() != 0)
            {
                foreach (var facilityInsurer in facilityInsurersList)
                {
                    var insurer = ObjectMapper.Map<CreateOrEditFacilityInsurerDto>(facilityInsurer);
                    insurer.InsuranceProviderName = facilityInsurer.InsuranceProviderFk?.Name;
                    insurer.InsuranceProviderType = facilityInsurer.InsuranceProviderFk?.Type;
                    insurers.Add(insurer);
                }

                return new GetFacilityInsurersForEditOutput()
                {
                    FacilityInsurers = insurers,
                    FacilityGroupName = facilityInsurersList.Select(o => o.FacilityGroupFk?.Name).First(),
                    FacilityName = facilityInsurersList.Select(o => o.FacilityFk?.Name).First()
                };
            }

            return new GetFacilityInsurersForEditOutput();
        }

        public async Task CreateOrEditMultiple(List<CreateOrEditFacilityInsurerDto> input)
        {
            foreach (CreateOrEditFacilityInsurerDto insurer in input)
            {
                await CreateOrEdit(insurer);
            }
        }

        public async Task CreateOrEdit(CreateOrEditFacilityInsurerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_FacilityInsurers_Create)]
        protected virtual async Task Create(CreateOrEditFacilityInsurerDto input)
        {
            var facilityInsurer = ObjectMapper.Map<FacilityInsurer>(input);

            if (AbpSession.TenantId != null)
            {
                facilityInsurer.TenantId = (int)AbpSession.TenantId;
            }

            var dbInsurerExists = _facilityInsurerRepository.GetAll()
                .WhereIf(input?.InsuranceProviderId != null, e => input.InsuranceProviderId == e.InsuranceProviderId).Any();

            if (dbInsurerExists)
            {
                throw new UserFriendlyException(L("InsuranceProviderHasBeenAddedAlready", input.InsuranceProviderId.ToString()));
            }

            await _facilityInsurerRepository.InsertAsync(facilityInsurer);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityInsurers_Edit)]
        protected virtual async Task Update(CreateOrEditFacilityInsurerDto input)
        {
            var facilityInsurer = await _facilityInsurerRepository.FirstOrDefaultAsync(
                (long)input.Id
            );
            ObjectMapper.Map(input, facilityInsurer);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityInsurers_Edit)]

        public virtual async Task ActivateOrDeactivateFacilityInsurer(ActivateOrDeactivateFacilityInsurerRequest input)
        {
            await _activateFacilityInsurer.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityInsurers_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _facilityInsurerRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityInsurers)]
        public async Task<
            PagedResultDto<FacilityInsurerFacilityGroupLookupTableDto>
        > GetAllFacilityGroupForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_facilityGroupRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var facilityGroupList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<FacilityInsurerFacilityGroupLookupTableDto>();
            foreach (var facilityGroup in facilityGroupList)
            {
                lookupTableDtoList.Add(
                    new FacilityInsurerFacilityGroupLookupTableDto
                    {
                        Id = facilityGroup.Id,
                        DisplayName = facilityGroup.Name?.ToString()
                    }
                );
            }

            return new PagedResultDto<FacilityInsurerFacilityGroupLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityInsurers)]
        public async Task<
            PagedResultDto<FacilityInsurerFacilityLookupTableDto>
        > GetAllFacilityForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_facilityRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var facilityList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<FacilityInsurerFacilityLookupTableDto>();
            foreach (var facility in facilityList)
            {
                lookupTableDtoList.Add(
                    new FacilityInsurerFacilityLookupTableDto
                    {
                        Id = facility.Id,
                        DisplayName = facility.Name?.ToString()
                    }
                );
            }

            return new PagedResultDto<FacilityInsurerFacilityLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}
