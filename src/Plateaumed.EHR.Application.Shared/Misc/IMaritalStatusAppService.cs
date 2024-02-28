using System.Collections.Generic;
using Abp.Application.Services;

namespace Plateaumed.EHR.Misc
{
    public interface IMaritalStatusAppService : IApplicationService
    {
        List<string> GetMaritalStatuses();
    }
}
