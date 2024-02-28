namespace Plateaumed.EHR.MultiTenancy.Dto;

public class TenantOnboardingProgressDto
{
    public TenantOnboardingStatus TenantOnboardingStatus { get; set; }

    public bool Completed { get; set; }
}