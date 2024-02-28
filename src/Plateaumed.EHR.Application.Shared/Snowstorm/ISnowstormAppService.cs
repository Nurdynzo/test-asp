using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Snowstorm.Dtos;

namespace Plateaumed.EHR.Snowstorm;

public interface ISnowstormAppService : IApplicationService
{
    Task<List<SnowstormSimpleResponseDto>> GetDiagnosisByTerm(string searchTerm);
}