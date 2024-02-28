using System.Collections.Generic;
using Abp.Application.Services;

namespace Plateaumed.EHR.Misc
{
    public interface IBloodGroupAppService : IApplicationService
    {
        List<string> GetBloodGroups();

        List<string> GetBloodGroupsBySearch(string searchText);
    }
}
