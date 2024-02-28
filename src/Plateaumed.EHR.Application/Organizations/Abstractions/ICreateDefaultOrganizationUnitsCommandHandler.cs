using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Organizations.Abstractions;

/// <summary>
/// Handler for creating default organization units
/// </summary>
public interface ICreateDefaultOrganizationUnitsCommandHandler : ITransientDependency
{
    /// <summary>
    /// Handle the command
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="facilityId"></param>
    /// <returns></returns>
    Task Handle(int tenantId, long facilityId);
}