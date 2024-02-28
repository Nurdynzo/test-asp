using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Dto;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Plateaumed.EHR.Storage;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_FacilityStaff)]
    public class FacilityStaffAppService : EHRAppServiceBase, IFacilityStaffAppService
    {
        private readonly IRepository<FacilityStaff, long> _facilityStaffRepository;
        private readonly IRepository<Facility, long> _lookup_facilityRepository;
        private readonly IRepository<StaffMember, long> _lookup_staffMemberRepository;

        public FacilityStaffAppService(
            IRepository<FacilityStaff, long> facilityStaffRepository,
            IRepository<Facility, long> lookup_facilityRepository,
            IRepository<StaffMember, long> lookup_staffMemberRepository
        )
        {
            _facilityStaffRepository = facilityStaffRepository;
            _lookup_facilityRepository = lookup_facilityRepository;
            _lookup_staffMemberRepository = lookup_staffMemberRepository;
        }

        public async Task<PagedResultDto<GetFacilityStaffForViewDto>> GetAll(
            GetAllFacilityStaffInput input
        )
        {
            var filteredFacilityStaff = _facilityStaffRepository
                .GetAll()
                .Include(e => e.FacilityFk)
                .Include(e => e.StaffMemberFk)
                .Include(e => e.StaffMemberFk.UserFk)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e =>
                        e.FacilityFk.Name.ToLower().Contains(input.Filter.ToLower())
                        || (
                            e.StaffMemberFk.StaffCode.ToLower().Contains(input.Filter.ToLower())
                            || e.StaffMemberFk.UserFk.FullName
                                .ToLower()
                                .Contains(input.Filter.ToLower())
                        )
                );

            var pagedAndFilteredFacilityStaff = filteredFacilityStaff
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var facilityStaff =
                from o in pagedAndFilteredFacilityStaff
                join o1 in _lookup_facilityRepository.GetAll() on o.FacilityId equals o1.Id into j1
                from s1 in j1.DefaultIfEmpty()
                join o2 in _lookup_staffMemberRepository.GetAll()
                    on o.StaffMemberId equals o2.Id
                    into j2
                from s2 in j2.DefaultIfEmpty()
                select new
                {
                    Id = o.Id,
                    FacilityName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                    StaffMemberStaffCode = s2 == null || s2.StaffCode == null
                        ? ""
                        : s2.StaffCode.ToString()
                };

            var totalCount = await filteredFacilityStaff.CountAsync();

            var dbList = await facilityStaff.ToListAsync();
            var results = new List<GetFacilityStaffForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetFacilityStaffForViewDto()
                {
                    FacilityStaff = new FacilityStaffDto { Id = o.Id, },
                    FacilityName = o.FacilityName,
                    StaffMemberStaffCode = o.StaffMemberStaffCode
                };

                results.Add(res);
            }

            return new PagedResultDto<GetFacilityStaffForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityStaff_Edit)]
        public async Task<GetFacilityStaffForEditOutput> GetFacilityStaffForEdit(
            EntityDto<long> input
        )
        {
            var facilityStaff = await _facilityStaffRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetFacilityStaffForEditOutput
            {
                FacilityStaff = ObjectMapper.Map<CreateOrEditFacilityStaffDto>(facilityStaff)
            };

            if (output.FacilityStaff.FacilityId != 0)
            {
                var _lookupFacility = await _lookup_facilityRepository.FirstOrDefaultAsync(
                    (long)output.FacilityStaff.FacilityId
                );
                output.FacilityName = _lookupFacility?.Name?.ToString();
            }

            if (output.FacilityStaff.StaffMemberId != 0)
            {
                var _lookupStaffMember = await _lookup_staffMemberRepository.FirstOrDefaultAsync(
                    output.FacilityStaff.StaffMemberId
                );
                output.StaffMemberStaffCode = _lookupStaffMember?.StaffCode?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditFacilityStaffDto input)
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

        [AbpAuthorize(AppPermissions.Pages_FacilityStaff_Create)]
        protected virtual async Task Create(CreateOrEditFacilityStaffDto input)
        {
            var facilityStaff = ObjectMapper.Map<FacilityStaff>(input);

            await _facilityStaffRepository.InsertAsync(facilityStaff);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityStaff_Edit)]
        protected virtual async Task Update(CreateOrEditFacilityStaffDto input)
        {
            var facilityStaff = await _facilityStaffRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, facilityStaff);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityStaff_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _facilityStaffRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityStaff)]
        public async Task<
            PagedResultDto<FacilityStaffFacilityLookupTableDto>
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

            var lookupTableDtoList = new List<FacilityStaffFacilityLookupTableDto>();
            foreach (var facility in facilityList)
            {
                lookupTableDtoList.Add(
                    new FacilityStaffFacilityLookupTableDto
                    {
                        Id = facility.Id,
                        DisplayName = facility.Name?.ToString()
                    }
                );
            }

            return new PagedResultDto<FacilityStaffFacilityLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityStaff)]
        public async Task<
            PagedResultDto<FacilityStaffStaffMemberLookupTableDto>
        > GetAllStaffMemberForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_staffMemberRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.StaffCode != null && e.StaffCode.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var staffMemberList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<FacilityStaffStaffMemberLookupTableDto>();
            foreach (var staffMember in staffMemberList)
            {
                lookupTableDtoList.Add(
                    new FacilityStaffStaffMemberLookupTableDto
                    {
                        Id = staffMember.Id,
                        DisplayName = staffMember.StaffCode?.ToString()
                    }
                );
            }

            return new PagedResultDto<FacilityStaffStaffMemberLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}
