using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Handlers;

/// <inheritdoc />
public class UpdateJobTitleCommandHandler : IUpdateJobTitleCommandHandler
{
    private readonly IRepository<JobTitle, long> _jobTitleRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="jobTitleRepository"></param>
    public UpdateJobTitleCommandHandler(IRepository<JobTitle, long> jobTitleRepository)
    {
        _jobTitleRepository = jobTitleRepository;
    }

    /// <inheritdoc />
    public async Task Handle(CreateOrEditJobTitleDto request)
    {
        var jobTitle = await _jobTitleRepository.FirstOrDefaultAsync(request.Id.Value)
                       ?? throw new UserFriendlyException("Job title not found");

        if (jobTitle.IsStatic && !ActivationChangeOnly(request, jobTitle))
            throw new UserFriendlyException("Cannot update static job title");

        jobTitle.Name = request.Name;
        jobTitle.ShortName = request.ShortName;
        jobTitle.IsActive = request.IsActive;

        await _jobTitleRepository.UpdateAsync(jobTitle);
    }

    private static bool ActivationChangeOnly(CreateOrEditJobTitleDto request, JobTitle jobTitle)
    {
        return (jobTitle.Name == request.Name || jobTitle.ShortName == request.ShortName) &&
               jobTitle.IsActive != request.IsActive;
    }
}