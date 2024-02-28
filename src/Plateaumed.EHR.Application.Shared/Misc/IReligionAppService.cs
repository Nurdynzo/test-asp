

using System.Collections.Generic;
using Abp.Application.Services;

namespace Plateaumed.EHR.Misc
{
    public interface IReligionAppService : IApplicationService
    {

        List<string> GetReligions();
    }
}
