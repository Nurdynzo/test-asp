using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

/// <summary>
/// Handler for getting all job titles
/// </summary>
public interface IGetJobTitlesQueryHandler : ITransientDependency
{
    /// <summary>
    /// Handle the query
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PagedResultDto<JobTitleDto>> Handle(GetAllJobTitlesInput request);
}