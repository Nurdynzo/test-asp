using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Handlers
{
    public class CreateJobCommandHandler : ICreateJobCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserManager _userManager;
        private readonly IRepository<Ward, long> _wardRepository;
        private readonly Abstractions.ISetStaffRolesCommandHandler _setStaffRoles;

        public CreateJobCommandHandler(IUserRepository userRepository, IObjectMapper objectMapper, IUserManager userManager,
            IRepository<Ward, long> wardRepository, Abstractions.ISetStaffRolesCommandHandler setStaffRoles)
        {
            _userRepository = userRepository;
            _objectMapper = objectMapper;
            _userManager = userManager;
            _wardRepository = wardRepository;
            _setStaffRoles = setStaffRoles;
        }

        public async Task Handle(CreateOrEditJobRequest request)
        {
            var user = await GetUser(request);
            var job = _objectMapper.Map<Job>(request.Job);

            if (request.Job.IsPrimary)
            {
                user.StaffMemberFk.Jobs.ForEach(j => j.IsPrimary = false);
            }

            await _setStaffRoles.SetTeamRole(job, request.Job.TeamRole);

            await SetWards(request, job);

            SetServiceCentres(request, job);

            user.StaffMemberFk.Jobs.Add(job);
            await _userManager.UpdateAsync(user);
            await _setStaffRoles.Handle(user.StaffMemberFk.Id);
        }

        private static void SetServiceCentres(CreateOrEditJobRequest request, Job job)
        {
            request.Job.ServiceCentres.ForEach(s =>
                job.JobServiceCentres.Add(new JobServiceCentre { ServiceCentre = s, Job = job }));
        }

        private async Task SetWards(CreateOrEditJobRequest request, Job job)
        {
            foreach (var w in request.Job.Wards)
            {
                var ward = await _wardRepository.GetAsync(w) ?? throw new UserFriendlyException("Ward not found");
                job.WardsJobs.Add(new WardJob { Ward = ward, Job = job });
            }
        }

        private async Task<User> GetUser(CreateOrEditJobRequest request)
        {
            var user = await _userRepository.GetAll().Include(u => u.StaffMemberFk.Jobs)
                           .FirstOrDefaultAsync(u => u.Id == request.UserId) ??
                       throw new UserFriendlyException("User not found");

            return user.StaffMemberFk == null
                ? throw new UserFriendlyException("User is not a staff member and cannot be assigned jobs")
                : user;
        }
    }
}