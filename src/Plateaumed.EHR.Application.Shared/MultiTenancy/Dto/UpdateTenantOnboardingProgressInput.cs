using System.Collections.Generic;

namespace Plateaumed.EHR.MultiTenancy.Dto
{
    public class UpdateTenantOnboardingProgressInput
    {
        public List<TenantOnboardingProgressDto> OnboardingProgress { get; set; }
    }
}
