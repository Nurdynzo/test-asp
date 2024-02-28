using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Staff.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Plateaumed.EHR.Staff.Abstractions;

namespace Plateaumed.EHR.Staff
{
    [AbpAuthorize(AppPermissions.Pages_JobTitles)]
    public class JobTitlesAppService : EHRAppServiceBase, IJobTitlesAppService
    {
        private readonly IRepository<JobTitle, long> _jobTitleRepository;
        private readonly ICreateJobTitleCommandHandler _createJobTitleCommandHandler;
        private readonly IUpdateJobTitleCommandHandler _updateJobTitleCommandHandler;
        private readonly IGetJobTitlesQueryHandler _getJobTitlesQueryHandler;

        public JobTitlesAppService(IRepository<JobTitle, long> jobTitleRepository, 
            ICreateJobTitleCommandHandler createJobTitleCommandHandler, 
            IUpdateJobTitleCommandHandler updateJobTitleCommandHandler, 
            IGetJobTitlesQueryHandler getJobTitlesQueryHandler)
        {
            _updateJobTitleCommandHandler = updateJobTitleCommandHandler;
            _getJobTitlesQueryHandler = getJobTitlesQueryHandler;
            _jobTitleRepository = jobTitleRepository;
            _createJobTitleCommandHandler = createJobTitleCommandHandler;
        }

        public async Task<PagedResultDto<JobTitleDto>> GetAll(GetAllJobTitlesInput input)
        {
            return await _getJobTitlesQueryHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_JobTitles_Edit)]
        public async Task<GetJobTitleForEditOutput> GetJobTitleForEdit(EntityDto<long> input)
        {
            var jobTitle = await _jobTitleRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetJobTitleForEditOutput
            {
                JobTitle = ObjectMapper.Map<CreateOrEditJobTitleDto>(jobTitle)
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditJobTitleDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_JobTitles_Create)]
        protected virtual async Task Create(CreateOrEditJobTitleDto input)
        {
            await _createJobTitleCommandHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_JobTitles_Edit)]
        protected virtual async Task Update(CreateOrEditJobTitleDto input)
        {
            await _updateJobTitleCommandHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_JobTitles_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _jobTitleRepository.DeleteAsync(input.Id);
        }
    }
}
