using System.Collections.Generic;

namespace Plateaumed.EHR.Authorization.Roles.Dto
{
    public class GetRolesInput
    {
        public List<string> Permissions { get; set; }
    }
}
