using System.Threading.Tasks;
using Abp.Dependency;
using Microsoft.AspNetCore.Identity;

namespace Plateaumed.EHR.Authorization.Users;

public interface IUserManager: ITransientDependency
{
    Task<string> CreateRandomPassword();
    Task<IdentityResult> CreateAsync(User user);
    Task<IdentityResult> UpdateAsync(User user);
    Task<IdentityResult> SetRolesAsync(User user, string[] roleNames);
}