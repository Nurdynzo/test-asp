using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;
using Stripe;

namespace Plateaumed.EHR.Staff.Handlers;

public class UpdateStaffMemberCommandHandler : IUpdateStaffMemberCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IRepository<Ward, long> _wardRepository;
    private readonly IUserManager _userManager;
    private readonly ISetStaffRolesCommandHandler _setStaffRoles;

    public UpdateStaffMemberCommandHandler(IUserRepository userRepository, IObjectMapper objectMapper,
        IRepository<Ward, long> wardRepository, IUserManager userManager, ISetStaffRolesCommandHandler setStaffRoles)
    {
        _userManager = userManager;
        _setStaffRoles = setStaffRoles;
        _wardRepository = wardRepository;
        _userRepository = userRepository;
        _objectMapper = objectMapper;
    }

    public async Task Handle(CreateOrEditStaffMemberRequest request)
    {
        var user = await _userRepository.GetAll()
                       .Include(x => x.Roles)
                       .Include(x => x.StaffMemberFk.Jobs)
                       .ThenInclude(x => x.JobServiceCentres)
                       .Include(x => x.StaffMemberFk.Jobs)
                       .ThenInclude(x => x.WardsJobs)
                       .Include(x => x.StaffMemberFk.Jobs)
                       .ThenInclude(x => x.TeamRole)
                       .FirstOrDefaultAsync(u => u.Id == request.User.Id)
                   ?? throw new UserFriendlyException("User not found");

        if (!request.Job.IsPrimary) throw new UserFriendlyException("Only primary jobs can be edited with staff member");

        var primaryJob = user.StaffMemberFk.Jobs.First(j => j.IsPrimary);

        _objectMapper.Map(request.Job, primaryJob);

        if (request.Job.ServiceCentres != null) MapServiceCentres(request, primaryJob);

        if (request.Job.Wards != null)  await MapWards(request, primaryJob);

        _objectMapper.Map(request, user.StaffMemberFk);

        _objectMapper.Map(request.User, user);
        user.SetNormalizedNames();
        await _setStaffRoles.SetTeamRole(primaryJob, request.Job.TeamRole);
        await _setStaffRoles.SetAdminRole(user.StaffMemberFk, request.AdminRole);
        await _setStaffRoles.Handle(user.StaffMemberFk.Id);

        await _userManager.UpdateAsync(user);
    }

    private void MapServiceCentres(CreateOrEditStaffMemberRequest request, Job primaryJob)
    {
        foreach (var centre in request.Job.ServiceCentres.Where(centre =>
                     primaryJob.JobServiceCentres.All(x => x.ServiceCentre != centre)))
        {
            primaryJob.JobServiceCentres.Add(new JobServiceCentre { ServiceCentre = centre, Job = primaryJob });
        }

        var jobServiceCentres = primaryJob.JobServiceCentres
            .Where(x => !request.Job.ServiceCentres.Contains(x.ServiceCentre)).ToList();
        jobServiceCentres.ForEach(x => primaryJob.JobServiceCentres.Remove(x));
    }

    private async Task MapWards(CreateOrEditStaffMemberRequest request, Job primaryJob)
    {
        foreach (var w in request.Job.Wards)
        {
            var ward = await _wardRepository.GetAsync(w) ?? throw new UserFriendlyException("Ward not found");
            if (primaryJob.WardsJobs.All(x => x.WardId != w))
            {
                primaryJob.WardsJobs.Add(new WardJob { WardId = ward.Id, Job = primaryJob });
            }
        }

        var removed = primaryJob.WardsJobs.Where(x => !request.Job.Wards.Contains(x.WardId)).ToList();
        foreach (var r in removed)
        {
            primaryJob.WardsJobs.Remove(r);
        }
    }
}