using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Permissions.Dto;

namespace Plateaumed.EHR.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}