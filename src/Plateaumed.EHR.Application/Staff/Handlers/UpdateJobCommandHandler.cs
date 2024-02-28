using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Handlers;

public class UpdateJobCommandHandler : IUpdateJobCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IUserManager _userManager;
    private readonly IRoleManager _roleManager;
    private readonly IRepository<Ward, long> _wardRepository;
    private readonly IRepository<Job, long> _jobRepository;
    private readonly Abstractions.ISetStaffRolesCommandHandler _setStaffRoles;

    public UpdateJobCommandHandler(IUserRepository userRepository,
        IObjectMapper objectMapper, IUserManager userManager, IRoleManager roleManager,
        IRepository<Ward, long> wardRepository, IRepository<Job, long> jobRepository, Abstractions.ISetStaffRolesCommandHandler setStaffRoles)
    {
        _userRepository = userRepository;
        _objectMapper = objectMapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _wardRepository = wardRepository;
        _jobRepository = jobRepository;
        _setStaffRoles = setStaffRoles;
    }

    public async Task Handle(CreateOrEditJobRequest request)
    {
        var user = await GetUser(request);
        var job = await GetJob(request);

        if (request.Job.IsPrimary)
        {
            user.StaffMemberFk.Jobs.ForEach(j => j.IsPrimary = false);
        }

        _objectMapper.Map(request.Job, job);

        SetServiceCentres(request, job);
        await SetWards(request, job);

        await _setStaffRoles.SetTeamRole(job, request.Job.TeamRole);
        
        await _jobRepository.UpdateAsync(job);
        
        await _setStaffRoles.Handle(user.StaffMemberFk.Id);

        await _userManager.UpdateAsync(user);
    }

    private async Task<Job> GetJob(CreateOrEditJobRequest request)
    {
        if (request.Job.Id == null)
            throw new UserFriendlyException("Job Id cannot be null");

        var job = await _jobRepository.GetAsync(request.Job.Id.Value) 
                  ?? throw new UserFriendlyException("Job not found");

        if (!request.Job.IsPrimary && job.IsPrimary)
        {
            throw new UserFriendlyException("Staff member must have a primary job");
        }
        return job;
    }

    private void SetServiceCentres(CreateOrEditJobRequest request, Job job)
    {
        foreach (var centre in request.Job.ServiceCentres.Where(centre =>
                     job.JobServiceCentres.All(x => x.ServiceCentre != centre)))
        {
            job.JobServiceCentres.Add(new JobServiceCentre { ServiceCentre = centre, Job = job });
        }

        var jobServiceCentres = job.JobServiceCentres
            .Where(x => !request.Job.ServiceCentres.Contains(x.ServiceCentre)).ToList();
        jobServiceCentres.ForEach(x => job.JobServiceCentres.Remove(x));
    }

    private async Task SetWards(CreateOrEditJobRequest request, Job job)
    {
        foreach (var w in request.Job.Wards)
        {
            var ward = await _wardRepository.GetAsync(w) ?? throw new UserFriendlyException("Ward not found");
            if (job.WardsJobs.All(x => x.WardId != w))
            {
                job.WardsJobs.Add(new WardJob { WardId = ward.Id, Job = job });
            }
        }

        var removed = job.WardsJobs.Where(x => !request.Job.Wards.Contains(x.WardId)).ToList();
        foreach (var r in removed)
        {
            job.WardsJobs.Remove(r);
        }
    }

    private async Task<User> GetUser(CreateOrEditJobRequest request)
    {
        var user = await _userRepository.GetAll().Include(u => u.Roles)
                       .Include(u => u.StaffMemberFk.Jobs)
                       .ThenInclude(j => j.JobServiceCentres)
                       .Include(u => u.StaffMemberFk.Jobs)
                       .ThenInclude(j => j.WardsJobs)
                       .Include(u => u.StaffMemberFk.AdminRole)
                       .FirstOrDefaultAsync(u => u.Id == request.UserId) ??
                   throw new UserFriendlyException("User not found");

        return user.StaffMemberFk == null
            ? throw new UserFriendlyException("User is not a staff member and cannot be assigned jobs")
            : user;
    }
}