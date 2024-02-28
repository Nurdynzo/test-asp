using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Staff.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;

namespace Plateaumed.EHR.Staff
{
    [AbpAuthorize(AppPermissions.Pages_JobLevels)]
    public class JobLevelsAppService : EHRAppServiceBase, IJobLevelsAppService
    {
        private readonly IRepository<JobLevel, long> _jobLevelRepository;
        private readonly IRepository<JobTitle, long> _lookup_jobTitleRepository;

        public JobLevelsAppService(
            IRepository<JobLevel, long> jobLevelRepository,
            IRepository<JobTitle, long> lookup_jobTitleRepository
        )
        {
            _jobLevelRepository = jobLevelRepository;
            _lookup_jobTitleRepository = lookup_jobTitleRepository;
        }

        public async Task<PagedResultDto<GetJobLevelForViewDto>> GetAll(GetAllJobLevelsInput input)
        {
            var filteredJobLevels = _jobLevelRepository
                .GetAll()
                .Include(e => e.JobTitleFk)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e =>
                        false || e.Name.Contains(input.Filter) || e.ShortName.Contains(input.Filter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.Contains(input.NameFilter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ShortNameFilter),
                    e => e.ShortName.Contains(input.ShortNameFilter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.JobTitleNameFilter),
                    e => e.JobTitleFk != null && e.JobTitleFk.Name == input.JobTitleNameFilter
                );

            var pagedAndFilteredJobLevels = filteredJobLevels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var jobLevels =
                from o in pagedAndFilteredJobLevels
                join o1 in _lookup_jobTitleRepository.GetAll() on o.JobTitleId equals o1.Id into j1
                from s1 in j1.DefaultIfEmpty()
                select new
                {
                    o.Name,
                    o.Rank,
                    o.ShortName,
                    Id = o.Id,
                    JobTitleName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                    o.JobTitleId,
                };

            var totalCount = await filteredJobLevels.CountAsync();

            var dbList = await jobLevels.ToListAsync();
            var results = new List<GetJobLevelForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetJobLevelForViewDto()
                {
                    JobLevel = new JobLevelDto
                    {
                        Name = o.Name,
                        Rank = o.Rank,
                        ShortName = o.ShortName,
                        Id = o.Id,
                        JobTitleId = o.JobTitleId
                    },
                    JobTitleName = o.JobTitleName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetJobLevelForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_JobLevels_Edit)]
        public async Task<GetJobLevelForEditOutput> GetJobLevelForEdit(EntityDto<long> input)
        {
            var jobLevel = await _jobLevelRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetJobLevelForEditOutput
            {
                JobLevel = ObjectMapper.Map<CreateOrEditJobLevelDto>(jobLevel)
            };

            if (output.JobLevel.JobTitleId != 0)
            {
                var _lookupJobTitle = await _lookup_jobTitleRepository.FirstOrDefaultAsync(
                    output.JobLevel.JobTitleId
                );
                output.JobTitleName = _lookupJobTitle?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditJobLevelDto input)
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

        [AbpAuthorize(AppPermissions.Pages_JobLevels_Create)]
        protected virtual async Task Create(CreateOrEditJobLevelDto input)
        {
            var jobLevel = ObjectMapper.Map<JobLevel>(input);

            if (AbpSession.TenantId != null)
            {
                jobLevel.TenantId = (int)AbpSession.TenantId;
            }

            await _jobLevelRepository.InsertAsync(jobLevel);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_JobLevels_Edit)]
        protected virtual async Task Update(CreateOrEditJobLevelDto input)
        {
            var jobLevel = await _jobLevelRepository.FirstOrDefaultAsync((long)input.Id);

            var activationChange = jobLevel.IsActive != input.IsActive && jobLevel.Name == input.Name;

            if(jobLevel.IsStatic && !activationChange)
                throw new UserFriendlyException("Cannot update static job level");

            jobLevel.Name = input.Name;
            jobLevel.ShortName = input.ShortName;
            await _jobLevelRepository.UpdateAsync(jobLevel);
        }

        [AbpAuthorize(AppPermissions.Pages_JobLevels_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _jobLevelRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_JobLevels)]
        public async Task<
            PagedResultDto<JobLevelJobTitleLookupTableDto>
        > GetAllJobTitleForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_jobTitleRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var jobTitleList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<JobLevelJobTitleLookupTableDto>();
            foreach (var jobTitle in jobTitleList)
            {
                lookupTableDtoList.Add(
                    new JobLevelJobTitleLookupTableDto
                    {
                        Id = jobTitle.Id,
                        DisplayName = jobTitle.Name?.ToString()
                    }
                );
            }

            return new PagedResultDto<JobLevelJobTitleLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}
