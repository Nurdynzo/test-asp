using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Facilities
{
    public interface IBedTypesAppService : IApplicationService
    {
        Task<PagedResultDto<BedTypeDto>> GetAll(GetAllBedTypesInput input);

        Task<GetBedTypeForEditOutput> GetBedTypeForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditBedTypeDto input);

        Task Delete(EntityDto<long> input);
    }
}
