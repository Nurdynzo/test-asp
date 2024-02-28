using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Staff.Abstractions;

/// <summary>
/// Handler for creating default job hierarchy
/// </summary>
public interface ICreateDefaultJobHierarchyCommandHandler: ITransientDependency
{
    /// <summary>
    /// Handle the command
    /// </summary>
    Task Handle(int tenantId, long facilityId);
}