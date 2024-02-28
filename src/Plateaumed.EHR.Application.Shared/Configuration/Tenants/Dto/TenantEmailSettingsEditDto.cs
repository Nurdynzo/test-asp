using Abp.Auditing;
using Plateaumed.EHR.Configuration.Dto;

namespace Plateaumed.EHR.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}