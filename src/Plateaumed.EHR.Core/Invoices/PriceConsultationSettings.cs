using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;
namespace Plateaumed.EHR.Invoices
{
    [Table("PriceConsultationSettings")]
    public class PriceConsultationSetting : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }
        public bool IsFollowUpVisitEnabled { get; set; }
        public int FrequencyOfChargesTimes { get; set; }
        public int FrequencyOfChargeValue { get; set; }
        public PriceTimeFrequency UnitOfFrequencyChargeUnit { get; set; }
        public int PercentageOfFrequencyCharges { get; set; }
    }
}
