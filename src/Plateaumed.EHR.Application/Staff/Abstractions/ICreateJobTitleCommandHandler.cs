using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

/// <summary>
/// Create Job Title Command Handler
/// </summary>
public interface ICreateJobTitleCommandHandler : ITransientDependency
{
    /// <summary>
    /// Handle create job title command
    /// </summary>
    /// <param name="createOrEditJobTitleDto"></param>
    Task Handle(CreateOrEditJobTitleDto createOrEditJobTitleDto);
}