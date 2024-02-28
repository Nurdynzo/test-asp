using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.Invoices
{
    [Table("PriceMealPlanDefinitions")]
    public class PriceMealPlanDefinition : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string PlanType { get; set; }
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Description { get; set; }
        public long PriceMealSettingId { get; set; }
        [ForeignKey("PriceMealSettingId")]
        public PriceMealSetting PriceMealSetting { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
