using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

/// <summary>
/// Update Job Title Command Handler
/// </summary>
public interface IUpdateJobTitleCommandHandler : ITransientDependency
{
    /// <summary>
    /// Handle update job title command
    /// </summary>
    /// <param name="request"></param>
    Task Handle(CreateOrEditJobTitleDto request);
}