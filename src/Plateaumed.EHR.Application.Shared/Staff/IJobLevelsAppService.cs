using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Staff
{
    public interface IJobLevelsAppService : IApplicationService
    {
        Task<PagedResultDto<GetJobLevelForViewDto>> GetAll(GetAllJobLevelsInput input);

        Task<GetJobLevelForEditOutput> GetJobLevelForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditJobLevelDto input);

        Task Delete(EntityDto<long> input);

        Task<PagedResultDto<JobLevelJobTitleLookupTableDto>> GetAllJobTitleForLookupTable(
            GetAllForLookupTableInput input
        );
    }
}
