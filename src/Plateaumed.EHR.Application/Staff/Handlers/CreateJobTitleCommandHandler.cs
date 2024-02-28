using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using NUglify.Helpers;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Handlers
{
    /// <inheritdoc />
    public class CreateJobTitleCommandHandler : ICreateJobTitleCommandHandler
    {
        private readonly IRepository<JobTitle, long> _jobTitleRepository;
        private readonly IAbpSession _abpSession;
        private readonly IObjectMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobTitleRepository"></param>
        /// <param name="abpSession"></param>
        /// <param name="mapper"></param>
        public CreateJobTitleCommandHandler(IRepository<JobTitle, long> jobTitleRepository, IAbpSession abpSession,
            IObjectMapper mapper)
        {
            _jobTitleRepository = jobTitleRepository;
            _abpSession = abpSession;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task Handle(CreateOrEditJobTitleDto createOrEditJobTitleDto)
        {
            var jobTitle = _mapper.Map<JobTitle>(createOrEditJobTitleDto);
            jobTitle.TenantId = _abpSession.TenantId;
            jobTitle.JobLevels.ForEach(jl => jl.TenantId = _abpSession.TenantId);
            await _jobTitleRepository.InsertAsync(jobTitle);
        }
    }
}
