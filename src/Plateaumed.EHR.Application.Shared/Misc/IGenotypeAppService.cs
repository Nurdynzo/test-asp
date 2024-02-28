using System.Collections.Generic;
using Abp.Application.Services;

namespace Plateaumed.EHR.Misc
{
    public interface IGenotypeAppService : IApplicationService
    {
        List<string> GetGenotypes();

        List<string> GetGenotypesBySearch(string searchText);
    }
}
