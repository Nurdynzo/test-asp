using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Notifications;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Url;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Staff.Abstractions;


namespace Plateaumed.EHR.Staff
{
    [AbpAuthorize(AppPermissions.Pages_StaffMembers)]
    public class StaffMembersAppService : EHRAppServiceBase, IStaffMembersAppService
    {
        public IAppUrlService AppUrlService { get; set; }
        private readonly IRepository<StaffMember, long> _staffMemberRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly RoleManager _roleManager;
        private readonly IUserEmailer _userEmailer;
        private readonly IRepository<Role> _roleRepository;
        private readonly IGetAllStaffMembersQueryHandler _getAllStaffMembers;
        private readonly ICreateStaffMemberCommandHandler _createStaffMember;
        private readonly IUpdateStaffMemberCommandHandler _updateStaffMember;
        private readonly IGetStaffMemberQueryHandler _getStaffMember;
        private readonly IGetStaffMembersWithJobsHandler _getStaffMembersWithJobsHandler;
        private readonly IActivateOrDeactivateStaffMemberHandler _activateOrDeactivateStaffHandler;

        public StaffMembersAppService(
            IRepository<StaffMember, long> staffMemberRepository,
            IRepository<User, long> userRepository,
            IRepository<UserRole, long> userRoleRepository,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IAppNotifier appNotifier,
            RoleManager roleManager,
            IUserEmailer userEmailer,
            IRepository<Role> roleRepository,
            IGetAllStaffMembersQueryHandler getAllStaffMembers,
            ICreateStaffMemberCommandHandler createStaffMember,
            IUpdateStaffMemberCommandHandler updateStaffMember,
            IGetStaffMemberQueryHandler getStaffMember,
            IGetStaffMembersWithJobsHandler getStaffMembersWithJobsHandler,
            IActivateOrDeactivateStaffMemberHandler activateOrDeactivateStaffMemberHandler
        )
        {
            _createStaffMember = createStaffMember;
            _updateStaffMember = updateStaffMember;
            _getStaffMember = getStaffMember;
            _getAllStaffMembers = getAllStaffMembers;
            _staffMemberRepository = staffMemberRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _roleRepository = roleRepository;
            _getStaffMembersWithJobsHandler = getStaffMembersWithJobsHandler;
            _activateOrDeactivateStaffHandler = activateOrDeactivateStaffMemberHandler;
        }

        public async Task<PagedResultDto<GetStaffMembersResponse>> GetAll(GetAllStaffMembersRequest request)
        {
           return await _getAllStaffMembers.Handle(request);
        }

        public async Task<PagedResultDto<GetStaffMemberResponse>> GetAllStaffWithJobs(GetStaffMembersWithJobsRequest request)
        {
            return await _getStaffMembersWithJobsHandler.Handle(request);
        }

        [AbpAuthorize(AppPermissions.Pages_StaffMembers)]
        public async Task<GetStaffMemberForEditResponse> GetStaffMember(EntityDto<long> input)
        {
            return await _getStaffMember.Handle(input);
        }
        
        public async Task CreateOrEdit(CreateOrEditStaffMemberRequest input)
        {
            if (input.User.Id.HasValue)
            {
                await Update(input);
            }
            else
            {
                await Create(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_StaffMembers_Create)]
        protected async Task Create(CreateOrEditStaffMemberRequest request)
        {
            var user = await _createStaffMember.Handle(request, CheckErrors);
            await CurrentUnitOfWork.SaveChangesAsync();

            await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(user.ToUserIdentifier());
            await _appNotifier.WelcomeToTheApplicationAsync(user);

            user.SetNewPasswordResetCode();
            await _userEmailer.SendEmailActivationLinkToStaffAsync(user,
                AppUrlService.CreatePasswordResetUrlFormat(AbpSession.TenantId));
        }

        [AbpAuthorize(AppPermissions.Pages_StaffMembers_Edit)]
        protected async Task Update(CreateOrEditStaffMemberRequest request)
        {
            await _updateStaffMember.Handle(request);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_StaffMembers_Edit)]
        public virtual async Task ActivateOrDeactivateStaffMember(ActivateOrDeactivateStaffMemberRequest input)
        {
            await _activateOrDeactivateStaffHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_StaffMembers_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _staffMemberRepository.DeleteAsync(input.Id);
        }

        

        public async Task<List<StaffMemberForReturnDto>> GetStaffMembersByRole(string roleName)
        {
            var role = await _roleRepository.GetAll()
                .Where(v => v.TenantId == AbpSession.TenantId)
                .WhereIf(!string.IsNullOrWhiteSpace(roleName), v => v.Name.ToLower().Equals(roleName.ToLower()))
                .Select(v => new Role
                    {
                        Id = v.Id,
                        DisplayName = v.DisplayName,
                        Name = v.Name,
                        TenantId = v.TenantId
                    }
                ).FirstOrDefaultAsync();

            role = await _roleManager.Roles.SingleOrDefaultAsync(v => v.Id == role.Id);

            // check if role is null
            if (role == null)
                throw new UserFriendlyException("User role not found.");

            // get all user based on the current selected role  
            var staffUserList = await
                (from user in _userRepository.GetAll().Include(u => u.StaffMemberFk)
                    join userRole in _userRoleRepository.GetAll() on user.Id equals userRole.UserId
                    where userRole.RoleId == role.Id
                    select user).ToListAsync();

            // map user to return dto  
            var staffMembers = ObjectMapper.Map<List<StaffMemberForReturnDto>>(staffUserList);

            // return list
            return staffMembers;
        }
    }
}