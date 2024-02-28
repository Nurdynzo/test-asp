using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dto;

namespace Plateaumed.EHR.Facilities.Abstractions;

/// <summary>
/// Handler for the facility logo upload
/// </summary>
public interface IUploadFacilityLogoCommandHandler : ITransientDependency
{
    /// <summary>
    /// Handler for the facility logo upload
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Guid> Handle(UploadFacilityLogoRequest request);
}