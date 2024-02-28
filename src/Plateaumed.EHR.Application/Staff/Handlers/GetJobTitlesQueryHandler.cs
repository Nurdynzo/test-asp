using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Staff.Dtos;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Staff.Abstractions;

namespace Plateaumed.EHR.Staff.Handlers
{
    /// <inheritdoc />
    public class GetJobTitlesQueryHandler : IGetJobTitlesQueryHandler
    {
        private readonly IRepository<JobTitle, long> _jobTitleRepository;
        private readonly IObjectMapper _objectMapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobTitleRepository"></param>
        /// <param name="objectMapper"></param>
        public GetJobTitlesQueryHandler(IRepository<JobTitle, long> jobTitleRepository, IObjectMapper objectMapper)
        {
            _jobTitleRepository = jobTitleRepository;
            _objectMapper = objectMapper;
        }

        /// <inheritdoc />
        public async Task<PagedResultDto<JobTitleDto>> Handle(GetAllJobTitlesInput request)
        {
            var filteredJobTitles = _jobTitleRepository
                .GetAll()
                .WhereIf(
                    request.FacilityId != null, 
                    e => e.FacilityId == request.FacilityId)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(request.Filter),
                    e =>
                        e.Name.Contains(request.Filter) || e.ShortName.Contains(request.Filter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(request.NameFilter),
                    e => e.Name.Contains(request.NameFilter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(request.ShortNameFilter),
                    e => e.ShortName.Contains(request.ShortNameFilter)
                );

            if (request.IncludeLevels)
            {
                filteredJobTitles = filteredJobTitles.Include(x => x.JobLevels);
            }

            var pagedAndFilteredJobTitles = filteredJobTitles
                .OrderBy(request.Sorting ?? "id asc")
                .PageBy(request);

            var totalCount = await filteredJobTitles.CountAsync();

            var dbList = await pagedAndFilteredJobTitles.ToListAsync();
            var results = dbList.Select(o => _objectMapper.Map<JobTitleDto>(o)).ToList();

            return new PagedResultDto<JobTitleDto>(totalCount, results);
        }
    }
}
