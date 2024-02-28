using Abp.Organizations;

using Plateaumed.EHR.Misc;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Organizations.Dtos;
using Plateaumed.EHR.Dto;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Plateaumed.EHR.Storage;

namespace Plateaumed.EHR.Organizations
{
    [AbpAuthorize(AppPermissions.Pages_OrganizationUnitTimes)]
    public class OrganizationUnitTimesAppService : EHRAppServiceBase, IOrganizationUnitTimesAppService
    {
        private readonly IRepository<OrganizationUnitTime, long> _organizationUnitTimeRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;

        public OrganizationUnitTimesAppService(IRepository<OrganizationUnitTime, long> organizationUnitTimeRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository)
        {
            _organizationUnitTimeRepository = organizationUnitTimeRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;

        }

        public async Task<PagedResultDto<GetOrganizationUnitTimeForViewDto>> GetAll(GetAllOrganizationUnitTimesInput input)
        {
            var dayOfTheWeekFilter = input.DayOfTheWeekFilter.HasValue
                        ? (DaysOfTheWeek)input.DayOfTheWeekFilter
                        : default;

            var filteredOrganizationUnitTimes = _organizationUnitTimeRepository.GetAll()
                        .Include(e => e.OrganizationUnitExtendedFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.DayOfTheWeekFilter.HasValue && input.DayOfTheWeekFilter > -1, e => e.DayOfTheWeek == dayOfTheWeekFilter)
                        .WhereIf(input.MinOpeningTimeFilter != null, e => e.OpeningTime >= input.MinOpeningTimeFilter)
                        .WhereIf(input.MaxOpeningTimeFilter != null, e => e.OpeningTime <= input.MaxOpeningTimeFilter)
                        .WhereIf(input.MinClosingTimeFilter != null, e => e.ClosingTime >= input.MinClosingTimeFilter)
                        .WhereIf(input.MaxClosingTimeFilter != null, e => e.ClosingTime <= input.MaxClosingTimeFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitExtendedFk != null && e.OrganizationUnitExtendedFk.DisplayName == input.OrganizationUnitDisplayNameFilter);

            var pagedAndFilteredOrganizationUnitTimes = filteredOrganizationUnitTimes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var organizationUnitTimes = from o in pagedAndFilteredOrganizationUnitTimes
                                        join o1 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitExtendedId equals o1.Id into j1
                                        from s1 in j1.DefaultIfEmpty()

                                        select new
                                        {
                                            o.DayOfTheWeek,
                                            o.OpeningTime,
                                            o.ClosingTime,
                                            o.IsActive,
                                            Id = o.Id,
                                            OrganizationUnitDisplayName = s1 == null || s1.DisplayName == null ? "" : s1.DisplayName.ToString()
                                        };

            var totalCount = await filteredOrganizationUnitTimes.CountAsync();

            var dbList = await organizationUnitTimes.ToListAsync();
            var results = new List<GetOrganizationUnitTimeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetOrganizationUnitTimeForViewDto()
                {
                    OrganizationUnitTime = new OrganizationUnitTimeDto
                    {

                        DayOfTheWeek = o.DayOfTheWeek,
                        OpeningTime = o.OpeningTime,
                        ClosingTime = o.ClosingTime,
                        IsActive = o.IsActive,
                        Id = o.Id,
                    },
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetOrganizationUnitTimeForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_OrganizationUnitTimes_Edit)]
        public async Task<GetOrganizationUnitTimeForEditOutput> GetOrganizationUnitTimeForEdit(EntityDto<long> input)
        {
            var organizationUnitTime = await _organizationUnitTimeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetOrganizationUnitTimeForEditOutput { OrganizationUnitTime = ObjectMapper.Map<CreateOrEditOrganizationUnitTimeDto>(organizationUnitTime) };

            if (output.OrganizationUnitTime.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.OrganizationUnitTime.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditOrganizationUnitTimeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_OrganizationUnitTimes_Create)]
        protected virtual async Task Create(CreateOrEditOrganizationUnitTimeDto input)
        {
            var organizationUnitTime = ObjectMapper.Map<OrganizationUnitTime>(input);

            if (AbpSession.TenantId != null)
            {
                organizationUnitTime.TenantId = (int)AbpSession.TenantId;
            }

            await _organizationUnitTimeRepository.InsertAsync(organizationUnitTime);

        }

        [AbpAuthorize(AppPermissions.Pages_OrganizationUnitTimes_Edit)]
        protected virtual async Task Update(CreateOrEditOrganizationUnitTimeDto input)
        {
            var organizationUnitTime = await _organizationUnitTimeRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, organizationUnitTime);

        }

        [AbpAuthorize(AppPermissions.Pages_OrganizationUnitTimes_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _organizationUnitTimeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_OrganizationUnitTimes)]
        public async Task<PagedResultDto<OrganizationUnitTimeOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_organizationUnitRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.DisplayName != null && e.DisplayName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var organizationUnitList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<OrganizationUnitTimeOrganizationUnitLookupTableDto>();
            foreach (var organizationUnit in organizationUnitList)
            {
                lookupTableDtoList.Add(new OrganizationUnitTimeOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit.DisplayName?.ToString()
                });
            }

            return new PagedResultDto<OrganizationUnitTimeOrganizationUnitLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}