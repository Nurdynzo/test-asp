using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Staff.Abstractions;

namespace Plateaumed.EHR.Staff.Handlers;

/// <inheritdoc />
public class CreateDefaultJobHierarchyCommandHandler : ICreateDefaultJobHierarchyCommandHandler
{
    private readonly IRepository<JobTitle, long> _jobTitleRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="jobTitleRepository"></param>
    public CreateDefaultJobHierarchyCommandHandler(IRepository<JobTitle, long> jobTitleRepository)
    {
        _jobTitleRepository = jobTitleRepository;
    }

    /// <inheritdoc />
    public async Task Handle(int tenantId, long facilityId)
    {
        foreach (var jobTitle in StaticJobHierarchy.GetDefaultJobTitlesHierarchyForTenant(tenantId, facilityId))
            await _jobTitleRepository.InsertAsync(jobTitle);
    }
}