using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Permissions.Dto;

namespace Plateaumed.EHR.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}