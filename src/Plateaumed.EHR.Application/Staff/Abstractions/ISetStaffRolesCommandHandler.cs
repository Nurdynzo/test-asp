using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Staff.Abstractions;

public interface ISetStaffRolesCommandHandler : ITransientDependency
{
    Task Handle(long staffMemberId);
    Task SetTeamRole(Job job, string teamRole);
    Task SetAdminRole(StaffMember staffMember, string adminRole);
}