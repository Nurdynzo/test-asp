using Abp.Application.Services;
using Plateaumed.EHR.Misc.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Misc
{
    public interface IOccupationAppService : IApplicationService
    {
        Task<List<OccupationDto>> GetOccupations(); 
    }
}
