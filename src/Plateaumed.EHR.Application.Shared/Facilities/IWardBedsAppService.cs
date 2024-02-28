using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Facilities
{
    public interface IWardBedsAppService : IApplicationService
    {
        Task<PagedResultDto<GetWardBedForViewDto>> GetAll(GetAllWardBedsInput input);       

        Task<GetWardBedForEditOutput> GetWardBedForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditWardBedDto input);

        Task Delete(EntityDto<long> input);

        Task<List<GetWardBedCountResponse>> GetWardBedCount(GetWardBedCountRequest request);
    }
}
