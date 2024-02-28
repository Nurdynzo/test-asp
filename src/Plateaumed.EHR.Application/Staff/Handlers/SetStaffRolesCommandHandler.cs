using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Staff.Handlers;

public class SetStaffRolesCommandHandler : Abstractions.ISetStaffRolesCommandHandler
{
    private readonly IRepository<StaffMember, long> _repository;
    private readonly IRoleManager _roleManager;
    private readonly IUserManager _userManager;
    private readonly IUnitOfWorkManager _unitOfWork;

    public SetStaffRolesCommandHandler(IRepository<StaffMember, long> repository, 
        IRoleManager roleManager, IUserManager userManager, IUnitOfWorkManager unitOfWork)
    {
        _repository = repository;
        _roleManager = roleManager;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(long staffMemberId)
    {
        var staffMember = await _repository.GetAll()
                              .Include(x => x.UserFk.Roles)
                              .Include(x => x.Jobs)
                              .ThenInclude(x => x.JobLevel)
                              .ThenInclude(x => x.JobTitleFk)
                              .FirstOrDefaultAsync(x => x.Id == staffMemberId) ??
                          throw new UserFriendlyException("Staff member not found");

        staffMember.Jobs.ForEach(SetRoleFromJobTitle);

        var roles = staffMember.Jobs.Select(x => x.TeamRole?.Name)
            .Concat(staffMember.Jobs.Select(x => x.JobRole?.Name))
            .Append(staffMember.AdminRole?.Name)
            .Where(x => !string.IsNullOrWhiteSpace(x));

        staffMember.UserFk.Roles.Clear();
        await _unitOfWork.Current.SaveChangesAsync();
        await _userManager.SetRolesAsync(staffMember.UserFk, roles.ToArray());
    }

    public async Task SetTeamRole(Job job, string teamRole)
    {
        if(job == null) throw new UserFriendlyException("Job cannot be null");
        job.TeamRole = await _roleManager.FindByNameAsync(teamRole ?? "");
    }

    public async Task SetAdminRole(StaffMember staffMember, string adminRole)
    {
        if(staffMember == null) throw new UserFriendlyException("Staff member cannot be null");
        staffMember.AdminRole = await _roleManager.FindByNameAsync(adminRole ?? "");
    }
        
    private async void SetRoleFromJobTitle(Job j)
    {
        j.JobRole = await _roleManager.FindByNameAsync(j.JobLevel?.JobTitleFk?.Name ?? "");
    }
}