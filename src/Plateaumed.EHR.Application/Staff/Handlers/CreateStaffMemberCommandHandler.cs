using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Handlers
{
    public class CreateStaffMemberCommandHandler : ICreateStaffMemberCommandHandler
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<Ward, long> _wardRepository;
        private readonly IUserPolicy _userPolicy;
        private readonly IUserManager _userManager;
        private readonly IRepository<User, long> _userRepository;
        private readonly ISetStaffRolesCommandHandler _setStaffRoles;

        public CreateStaffMemberCommandHandler(IUserManager userManager,
            IObjectMapper objectMapper, IAbpSession abpSession, IPasswordHasher<User> passwordHasher,
            IRepository<Ward, long> wardRepository, IUserPolicy userPolicy, 
            IRepository<User, long> userRepository, ISetStaffRolesCommandHandler setStaffRoles)
        {
            _userManager = userManager;
            _abpSession = abpSession;
            _passwordHasher = passwordHasher;
            _wardRepository = wardRepository;
            _userPolicy = userPolicy;
            _objectMapper = objectMapper;
            _userRepository = userRepository;
            _setStaffRoles = setStaffRoles;
        }

        public async Task<User> Handle(CreateOrEditStaffMemberRequest request, Action<IdentityResult> checkErrors)
        {
            var tenantId = await CheckTenant();

            if (request.ContractEndDate != null)  CheckContractDate(request);
            
            await CheckStaffCode(request);
            
            var staffMember = _objectMapper.Map<StaffMember>(request);
            var job = await MapJob(request, tenantId);
            await _setStaffRoles.SetTeamRole(job, request.Job.TeamRole);
            await _setStaffRoles.SetAdminRole(staffMember, request.AdminRole);

            staffMember.Jobs.Add(job);

            if(job?.FacilityId != null)
            {
                staffMember.AssignedFacilities.Add(new FacilityStaff
                {
                    FacilityId = (long)job.FacilityId,
                    StaffMemberId = staffMember.Id,
                    IsDefault = true
                });
            }

            var user = await CreateUser(request, checkErrors, tenantId, staffMember);
            await _setStaffRoles.Handle(staffMember.Id);
            return user;
        }

        private async Task<User> CreateUser(CreateOrEditStaffMemberRequest request, Action<IdentityResult> checkErrors, int tenantId,
            StaffMember staffMember)
        {
            var user = _objectMapper.Map<User>(request.User);
            var randomPassword = await _userManager.CreateRandomPassword();

            user.Password = _passwordHasher.HashPassword(user, randomPassword);
            user.TenantId = tenantId;
            user.StaffMemberFk = staffMember;
            user.SetNormalizedNames();
            user.SetNewEmailConfirmationCode();
            user.ShouldChangePasswordOnNextLogin = true;
            checkErrors(await _userManager.CreateAsync(user));
            return user;
        }

        private async Task CheckStaffCode(CreateOrEditStaffMemberRequest request)
        {
            var facilityId = request.Job.FacilityId;

            var duplicate = await _userRepository.GetAll().Include(x => x.StaffMemberFk).ThenInclude(x => x.Jobs)
                                .AnyAsync(x => x.StaffMemberFk != null && x.StaffMemberFk.StaffCode.ToLower() == request.StaffCode.ToLower() && x.StaffMemberFk.Jobs.Any(j => j.FacilityId == facilityId));

            if(duplicate)
            {
                throw new UserFriendlyException("Staff code exists already within the tenant");
            }
        }

        private void CheckContractDate(CreateOrEditStaffMemberRequest request)
        {
            if (request.ContractStartDate != null && request.ContractEndDate != null && request.ContractStartDate > request.ContractEndDate)
            {
                throw new UserFriendlyException("Contract end date must be later than contract start date");
            }

            if (request.User.DateOfBirth != null && request.ContractStartDate != null && request.ContractStartDate < request.User.DateOfBirth)
            {
                throw new UserFriendlyException("Date of birth must be earlier than contract start date");
            }
        }

        private async Task<int> CheckTenant()
        {
            var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("TenantId cannot be null");
            await _userPolicy.CheckMaxUserCountAsync(tenantId);
            return tenantId;
        }

        private async Task<Job> MapJob(CreateOrEditStaffMemberRequest request, int tenantId)
        {
            var job = _objectMapper.Map<Job>(request.Job);
            job.TenantId = tenantId;
            job.IsPrimary = true;

            request.Job.ServiceCentres?.ForEach(s =>
                                job.JobServiceCentres.Add(new JobServiceCentre { ServiceCentre = s, Job = job }));

            if (request.Job.Wards != null)
            {
                foreach (var w in request.Job.Wards)
                {
                    var ward = await _wardRepository.GetAsync(w) ?? throw new UserFriendlyException("Ward not found");
                    job.WardsJobs.Add(new WardJob { Ward = ward, Job = job });
                }
            }

            return job;
        }
    }
}