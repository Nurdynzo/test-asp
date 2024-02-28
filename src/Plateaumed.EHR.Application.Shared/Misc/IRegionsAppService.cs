using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc.Dtos;

namespace Plateaumed.EHR.Misc
{
    public interface IRegionsAppService : IApplicationService
    {
        Task<PagedResultDto<GetRegionForViewDto>> GetAll(GetAllRegionsInput input);
        Task<ListResultDto<GetRegionsForListDto>> GetRegions(GetRegionsInput input);

        Task<List<GetRegionByCountryIdOutput>> GetRegionsByCountryId(int input);

        Task<GetRegionForEditOutput> GetRegionForEdit(EntityDto<int> input);
        Task CreateOrEdit(CreateOrEditRegionDto input);
        Task Delete(EntityDto<int> input);
    }
}