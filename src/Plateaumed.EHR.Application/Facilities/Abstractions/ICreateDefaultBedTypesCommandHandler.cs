using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Facilities.Abstractions;

/// <summary>
/// Handler to create default bed types for a facility
/// </summary>
public interface ICreateDefaultBedTypesCommandHandler: ITransientDependency
{
    /// <summary>
    /// Handle the command
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="facilityId"></param>
    /// <returns></returns>
    Task Handle(int tenantId, long facilityId);
}