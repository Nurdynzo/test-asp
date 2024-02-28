using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;
using IObjectMapper = Abp.ObjectMapping.IObjectMapper;

namespace Plateaumed.EHR.Staff.Handlers
{
    /// <inheritdoc />
    public class GetAllStaffMembersQueryHandler : IGetAllStaffMembersQueryHandler
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitsRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="objectMapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="organizationUnitsRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="userRoleRepository"></param>
        public GetAllStaffMembersQueryHandler(IObjectMapper objectMapper, IRepository<User, long> userRepository,
            IRepository<OrganizationUnitExtended, long> organizationUnitsRepository, IRepository<Role> roleRepository, IRepository<UserRole, long> userRoleRepository)
        {
            _organizationUnitsRepository = organizationUnitsRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _objectMapper = objectMapper;
        }

        /// <inheritdoc />
        public async Task<PagedResultDto<GetStaffMembersResponse>> Handle(GetAllStaffMembersRequest request)
        {
            var filterTerms = !string.IsNullOrWhiteSpace(request.Filter) ? request.Filter.ToLower().Split(" ") : null;

            var filteredStaffMembers = _userRepository
                .GetAll()
                .Include(u => u.StaffMemberFk)
                .Include(u => u.CountryFk)
                .Include(u => u.StaffMemberFk.Jobs)
                .ThenInclude(u => u.JobLevel.JobTitleFk)
                .Include(u => u.StaffMemberFk.Jobs)
                .ThenInclude(u => u.Department)
                .Include(u => u.StaffMemberFk.Jobs)
                .ThenInclude(u => u.Unit)
                .Include(u => u.StaffMemberFk.AssignedFacilities)
                .Include(u => u.OrganizationUnits)
                .Where(u => u.StaffMemberFk != null)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(request.Filter),
                    u => (u.Name.ToLower() + " " + (!string.IsNullOrEmpty(u.MiddleName) ? u.MiddleName + " " : string.Empty) + u.Surname.ToLower()).Contains(request.Filter.ToLower()) 
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(request.StaffCodeFilter),
                    u => u.StaffMemberFk.StaffCode.ToLower().Contains(request.StaffCodeFilter.ToLower())
                )
                .WhereIf(
                    request.MinContractStartDateFilter != null,
                    u => u.StaffMemberFk.ContractStartDate >= request.MinContractStartDateFilter
                )
                .WhereIf(
                    request.MaxContractStartDateFilter != null,
                    u => u.StaffMemberFk.ContractStartDate <= request.MaxContractStartDateFilter
                )
                .WhereIf(
                    request.MinContractEndDateFilter != null,
                    u => u.StaffMemberFk.ContractEndDate >= request.MinContractEndDateFilter
                )
                .WhereIf(
                    request.MaxContractEndDateFilter != null,
                    u => u.StaffMemberFk.ContractEndDate <= request.MaxContractEndDateFilter
                )
                .WhereIf(
                    request.JobTitleIdFilter.HasValue,
                    u => u.StaffMemberFk.Jobs.Any(j => j.JobLevel != null && j.JobLevel.JobTitleId == request.JobTitleIdFilter)
                )
                .WhereIf(
                    request.JobLevelIdFilter.HasValue,
                    u => u.StaffMemberFk.Jobs.Any(j => j.JobLevelId == request.JobLevelIdFilter)
                ).WhereIf(
                    request.FacilityIdFilter.HasValue,
                    u => u.StaffMemberFk.AssignedFacilities.Any(f => f.FacilityId == request.FacilityIdFilter)
                ).WhereIf(
                    request.Role.HasValue,
                    u => u.Roles.Any(r => r.RoleId == request.Role)
                );

            var pagedAndFilteredStaffMembers = filteredStaffMembers
                .OrderBy(request.Sorting ?? "id asc")
                .PageBy(request);

            var totalCount = await filteredStaffMembers.CountAsync();

            var staffMembers = await pagedAndFilteredStaffMembers.ToListAsync();
            
            var results = staffMembers.Select(user =>
                {
                    var primaryJob = user.StaffMemberFk.Jobs.FirstOrDefault(j => j.IsPrimary);
                    var getStaffMembersResponse = new GetStaffMembersResponse()
                    {
                        Id = user.StaffMemberFk.Id,
                        Title = user.Title,
                        JobTitle = primaryJob?.JobLevel?.JobTitleFk?.Name,
                        JobLevel = primaryJob?.JobLevel?.Name,
                        Department = primaryJob?.Department?.DisplayName,
                        Unit = primaryJob?.Unit?.DisplayName,
                        UnitId = primaryJob?.Unit?.Id,
                        StaffCode = user.StaffMemberFk.StaffCode,
                        ContractStartDate = user.StaffMemberFk.ContractStartDate,
                        ContractEndDate = user.StaffMemberFk.ContractEndDate,
                        IsActive = GetIsActive(user.StaffMemberFk),
                    };
                    _objectMapper.Map(user, getStaffMembersResponse);
                    return getStaffMembersResponse;
                })
                .ToList();

            return new PagedResultDto<GetStaffMembersResponse>(totalCount, results);
        }

        private bool GetIsActive(StaffMember staffMember)
        {
            return staffMember.ContractStartDate <= Clock.Now && staffMember.ContractEndDate >= Clock.Now;
        }

        public async Task<List<GetStaffMembersSimpleResponseDto>> SearchStaffHandle(string searchFilter, bool isAnaethetist, IAbpSession abpSession)
        {
            IQueryable<User> filteredStaffMembers = default;

            if (!isAnaethetist)
            { 
                filteredStaffMembers = _userRepository
                    .GetAll()
                    .Include(u => u.StaffMemberFk)     
                    .Where(u => u.StaffMemberFk != null)
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(searchFilter),
                        u => u.Name.ToLower().Contains(searchFilter) || u.MiddleName.ToLower().Contains(searchFilter) ||
                             u.Surname.ToLower().Contains(searchFilter) ||
                             u.StaffMemberFk.StaffCode.ToLower().Contains(searchFilter)
                    );
            }
            else
            {
                var role = await _roleRepository.GetAll()
                    .Where(v => v.TenantId == abpSession.TenantId && v.Name.ToLower().Equals(StaticRoleNames.JobRoles.Anaethetist))
                    .FirstOrDefaultAsync(); 
                
                // check if role is null
                if (role == null)
                    throw new UserFriendlyException($"Role '{StaticRoleNames.JobRoles.Anaethetist}' has not been configured for this tenant.");
                
                // get all user based on the current selected role  
                filteredStaffMembers = (from u in _userRepository.GetAll().Include(u => u.StaffMemberFk)
                        join ur in _userRoleRepository.GetAll() on u.Id equals ur.UserId
                        where ur.RoleId == role.Id 
                              && (u.Name.ToLower().Contains(searchFilter) 
                                  || u.MiddleName.ToLower().Contains(searchFilter) 
                                  || u.Surname.ToLower().Contains(searchFilter) 
                                  || u.StaffMemberFk.StaffCode.ToLower().Contains(searchFilter))
                        select u);
            } 
           

            var result = await filteredStaffMembers.Take(15).ToListAsync();
            return _objectMapper.Map<List<GetStaffMembersSimpleResponseDto>>(result);
        }
    }
}