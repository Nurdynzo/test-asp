using System;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Organizations;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Organizations.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Organizations.Abstractions;

namespace Plateaumed.EHR.Organizations
{
    [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
    public class OrganizationUnitAppService : EHRAppServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitExtendedRepository;
        private readonly IRepository<OrganizationUnitTime, long> _organizationUnitTimeRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<OrganizationUnitRole, long> _organizationUnitRoleRepository;
        private readonly IGetCurrentUserFacilityIdQueryHandler _getCurrentUserFacilityId;
        private readonly IGetOrganizationUnitsQueryHandler _getOrganizationUnits;
        private readonly RoleManager _roleManager;

        public OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<OrganizationUnitExtended, long> organizationUnitExtendedRepository,
            IRepository<OrganizationUnitTime, long> organizationUnitTimeRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            RoleManager roleManager,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository,
            IGetCurrentUserFacilityIdQueryHandler getCurrentUserFacilityId,
            IGetOrganizationUnitsQueryHandler getOrganizationUnits)
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _organizationUnitExtendedRepository = organizationUnitExtendedRepository;
            _organizationUnitTimeRepository = organizationUnitTimeRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _roleManager = roleManager;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
            _getCurrentUserFacilityId = getCurrentUserFacilityId;
            _getOrganizationUnits = getOrganizationUnits;
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits(GetOrganizationUnitsInput input)
        {
            return await _getOrganizationUnits.Handle(input, AbpSession.TenantId.Value);
        }

        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(
            GetOrganizationUnitUsersInput input)
        {
            var query = from ouUser in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on ouUser.OrganizationUnitId equals ou.Id
                        join user in UserManager.Users on ouUser.UserId equals user.Id
                        where ouUser.OrganizationUnitId == input.Id
                        select new
                        {
                            ouUser,
                            user
                        };

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<OrganizationUnitUserListDto>(
                totalCount,
                items.Select(item =>
                {
                    var organizationUnitUserDto = ObjectMapper.Map<OrganizationUnitUserListDto>(item.user);
                    organizationUnitUserDto.AddedTime = item.ouUser.CreationTime;
                    return organizationUnitUserDto;
                }).ToList());
        }

        public async Task<PagedResultDto<OrganizationUnitRoleListDto>> GetOrganizationUnitRoles(
            GetOrganizationUnitRolesInput input)
        {
            var query = from ouRole in _organizationUnitRoleRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on ouRole.OrganizationUnitId equals ou.Id
                        join role in _roleManager.Roles on ouRole.RoleId equals role.Id
                        where ouRole.OrganizationUnitId == input.Id
                        select new
                        {
                            ouRole,
                            role
                        };

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<OrganizationUnitRoleListDto>(
                totalCount,
                items.Select(item =>
                {
                    var organizationUnitRoleDto = ObjectMapper.Map<OrganizationUnitRoleListDto>(item.role);
                    organizationUnitRoleDto.AddedTime = item.ouRole.CreationTime;
                    return organizationUnitRoleDto;
                }).ToList());
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            if (!Enum.TryParse(input.Type, out OrganizationUnitType unitType))
            {
                throw new UserFriendlyException("Invalid organization unit type");
            }
            
            var organizationUnitExtended =
                new OrganizationUnitExtended(AbpSession.TenantId, input.DisplayName, input.ParentId)
                {
                    IsActive = input.IsActive.GetValueOrDefault(true),
                    Type = unitType,
                    ShortName = input.ShortName,
                    FacilityId = input.FacilityId
                };

            await _organizationUnitManager.CreateAsync(organizationUnitExtended);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (organizationUnitExtended.Type == OrganizationUnitType.Clinic)
            {
                var operatingTimes = ObjectMapper.Map<OrganizationUnitTime[]>(input.OperatingTimes);
                organizationUnitExtended.OperatingTimes = operatingTimes;
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnitDto>(organizationUnitExtended);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input)
        {
            var organizationUnitExtended = await _organizationUnitExtendedRepository.GetAll()
                                               .Include(ou => ou.OperatingTimes)
                                               .FirstOrDefaultAsync(ou => ou.Id == input.Id)
                                           ?? throw new UserFriendlyException(L("OrganizationUnitNotFoundForThisUser"));

            if (organizationUnitExtended.IsStatic && (organizationUnitExtended.ShortName != input.ShortName ||
                                                      organizationUnitExtended.DisplayName != input.DisplayName))
                throw new UserFriendlyException("Cannot update static organization unit");

            organizationUnitExtended.DisplayName = input.DisplayName;
            organizationUnitExtended.ShortName = input.ShortName;

            organizationUnitExtended.IsActive = input.IsActive.GetValueOrDefault(true);

            await _organizationUnitManager.UpdateAsync(organizationUnitExtended);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (organizationUnitExtended.Type == OrganizationUnitType.Clinic)
            {
                //Delete removed operating times
                var incomingOperatingTimeIds = input.OperatingTimes.Select(ou => ou.Id).ToList();
                for (var index = 0; index < organizationUnitExtended.OperatingTimes.Count; index++)
                {
                    var existingOperatingTime = organizationUnitExtended.OperatingTimes.ElementAt(index);
                    var isToBeDeleted = !incomingOperatingTimeIds.Contains(existingOperatingTime.Id);

                    if (isToBeDeleted)
                    {
                        await _organizationUnitTimeRepository.DeleteAsync(existingOperatingTime.Id);
                    }
                }

                //Create new operating times
                var newOperatingTimes = ObjectMapper.Map<List<OrganizationUnitTime>>(input.OperatingTimes.Where(t => !t.Id.HasValue));
                foreach (var operatingTime in newOperatingTimes)
                {
                    organizationUnitExtended.OperatingTimes.Add(operatingTime);
                }

                //Update existing operating times
                var updatedOperatingTimes = input.OperatingTimes.Where(t => t.Id.HasValue);
                foreach (var operatingTime in updatedOperatingTimes)
                {
                    var existingOperatingTime = await _organizationUnitTimeRepository.GetAsync(operatingTime.Id.Value);
                    ObjectMapper.Map(operatingTime, existingOperatingTime);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return await MapToOrganizationUnitDto(organizationUnitExtended);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input)
        {
            await _organizationUnitManager.MoveAsync(input.Id, input.NewParentId);

            return await MapToOrganizationUnitDto(
                await _organizationUnitExtendedRepository.GetAsync(input.Id)
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task DeleteOrganizationUnit(EntityDto<long> input)
        {
            await _userOrganizationUnitRepository.DeleteAsync(x => x.OrganizationUnitId == input.Id);
            await _organizationUnitRoleRepository.DeleteAsync(x => x.OrganizationUnitId == input.Id);
            await _organizationUnitManager.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await UserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles)]
        public async Task RemoveRoleFromOrganizationUnit(RoleToOrganizationUnitInput input)
        {
            await _roleManager.RemoveFromOrganizationUnitAsync(input.RoleId, input.OrganizationUnitId);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task AddUsersToOrganizationUnit(UsersToOrganizationUnitInput input)
        {
            foreach (var userId in input.UserIds)
            {
                await UserManager.AddToOrganizationUnitAsync(userId, input.OrganizationUnitId);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles)]
        public async Task AddRolesToOrganizationUnit(RolesToOrganizationUnitInput input)
        {
            foreach (var roleId in input.RoleIds)
            {
                await _roleManager.AddToOrganizationUnitAsync(roleId, input.OrganizationUnitId, AbpSession.TenantId);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input)
        {
            var userIdsInOrganizationUnit = _userOrganizationUnitRepository.GetAll()
                .Where(uou => uou.OrganizationUnitId == input.OrganizationUnitId)
                .Select(uou => uou.UserId);

            var query = UserManager.Users
                .Where(u => !userIdsInOrganizationUnit.Contains(u.Id))
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter) ||
                        u.Surname.Contains(input.Filter) ||
                        u.UserName.Contains(input.Filter) ||
                        u.EmailAddress.Contains(input.Filter)
                );

            var userCount = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.Name)
                .ThenBy(u => u.Surname)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<NameValueDto>(
                userCount,
                users.Select(u =>
                    new NameValueDto(
                        u.FullName + " (" + u.EmailAddress + ")",
                        u.Id.ToString()
                    )
                ).ToList()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles)]
        public async Task<PagedResultDto<NameValueDto>> FindRoles(FindOrganizationUnitRolesInput input)
        {
            var roleIdsInOrganizationUnit = _organizationUnitRoleRepository.GetAll()
                .Where(uou => uou.OrganizationUnitId == input.OrganizationUnitId)
                .Select(uou => uou.RoleId);

            var query = _roleManager.Roles
                .Where(u => !roleIdsInOrganizationUnit.Contains(u.Id))
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.DisplayName.Contains(input.Filter) ||
                        u.Name.Contains(input.Filter)
                );

            var roleCount = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.DisplayName)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<NameValueDto>(
                roleCount,
                users.Select(u =>
                    new NameValueDto(
                        u.DisplayName,
                        u.Id.ToString()
                    )
                ).ToList()
            );
        }

        public async Task<List<OrganizationUnitDto>> GetAll()
        {
            var organizationUnitsExtended = await _organizationUnitExtendedRepository.GetAllListAsync();
            return ObjectMapper.Map<List<OrganizationUnitDto>>(organizationUnitsExtended);
        }

        private async Task<OrganizationUnitDto> MapToOrganizationUnitDto(OrganizationUnitExtended organizationUnit)
        {
            var dto = ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
            dto.OperatingTimes = ObjectMapper.Map<OrganizationUnitTimeDto[]>(await _organizationUnitTimeRepository
                .GetAll()
                .Where(t => t.OrganizationUnitExtendedId == organizationUnit.Id)
                .OrderBy(t => t.DayOfTheWeek)
                .ToListAsync());
            dto.MemberCount =
                await _userOrganizationUnitRepository.CountAsync(uou => uou.OrganizationUnitId == organizationUnit.Id);
            return dto;
        }

        public async Task<List<ClinicListDto>> GetClinics()
        {
            var facilityId = await _getCurrentUserFacilityId.Handle();
            return await _organizationUnitExtendedRepository.GetAll()
                .Where(x => x.Type == OrganizationUnitType.Clinic)
                .WhereIf(facilityId.HasValue, x => x.FacilityId == facilityId)
                .Select(x => ObjectMapper.Map<ClinicListDto>(x)).ToListAsync();
        }
    }
}