using Abp.Authorization;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
