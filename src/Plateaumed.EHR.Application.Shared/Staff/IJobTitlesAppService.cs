using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff
{
    public interface IJobTitlesAppService : IApplicationService
    {
        Task<PagedResultDto<JobTitleDto>> GetAll(GetAllJobTitlesInput input);

        Task<GetJobTitleForEditOutput> GetJobTitleForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditJobTitleDto input);

        Task Delete(EntityDto<long> input);
    }
}
