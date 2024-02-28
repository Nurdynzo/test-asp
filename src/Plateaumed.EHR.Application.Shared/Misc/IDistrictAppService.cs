using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc.Dtos;

namespace Plateaumed.EHR.Misc
{
    public interface IDistrictAppService : IApplicationService
    {
        Task<PagedResultDto<GetDistrictForViewDto>> GetAll(GetAllDistrictsInput input); 
        Task<ListResultDto<GetDistrictsForListDto>> GetDistricts(GetDistrictInput input); 
        Task<GetDistrictForEditOutput> GetDistrictForEdit(EntityDto<int> input); 
        Task CreateOrEdit(CreateOrEditDistrictDto input); 
        Task Delete(EntityDto<int> input);  
    } 
}