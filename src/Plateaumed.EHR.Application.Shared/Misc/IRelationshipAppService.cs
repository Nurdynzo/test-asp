using System.Collections.Generic;
using Abp.Application.Services;

namespace Plateaumed.EHR.Misc
{
    public interface IRelationshipAppService : IApplicationService
    {
        List<string> GetRelationships();
    }
}
