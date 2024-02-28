using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities
{
    public interface IWardsAppService : IApplicationService
    {
        Task<PagedResultDto<GetWardForViewDto>> GetAll(GetAllWardsInput input);

        Task<List<WardDto>> GetAllWards();

        Task ActivateOrDeactivateWard(ActivateOrDeactivateWardRequest input);

        Task<GetWardForEditOutput> GetWardForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditWardDto input);

        Task Delete(EntityDto<long> input);
    }
}
