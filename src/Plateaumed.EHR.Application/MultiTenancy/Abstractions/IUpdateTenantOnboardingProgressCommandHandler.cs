using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.MultiTenancy.Dto;

namespace Plateaumed.EHR.MultiTenancy.Abstractions;

/// <summary>
/// This handler is used to update the tenant onboarding progress
/// </summary>
public interface IUpdateTenantOnboardingProgressCommandHandler : ITransientDependency
{
    /// <summary>
    /// This method is used to update the tenant onboarding progress
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task Handle(UpdateTenantOnboardingProgressInput request);
}