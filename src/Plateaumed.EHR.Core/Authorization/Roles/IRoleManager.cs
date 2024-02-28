using System.Threading.Tasks;

namespace Plateaumed.EHR.Authorization.Roles;

public interface IRoleManager
{
    Task<Role> GetRoleByNameAsync(string roleName);

    Task<Role> FindByNameAsync(string roleName);
}