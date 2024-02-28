using System.Collections.Generic;

namespace Plateaumed.EHR.MultiTenancy.Dto
{
    public class GetTenantOnboardingProgressOutput
    {
        public bool IsOnboarded { get; set; }

        public List<TenantOnboardingProgressDto> OnboardingProgress { get; set; }
    }
}
