using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;
namespace Plateaumed.EHR.Invoices
{
    [Table("PriceWardAdmissionSettings")]
    public class PriceWardAdmissionSetting: FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }
        public int DefaultInitialPeriodValue { get; set; }
        public PriceTimeFrequency DefaultInitialPeriodUnit { get; set; }
        public PriceTimeFrequency DefaultContinuePeriodUnit { get; set; }
        public int DefaultContinuePeriodValue { get; set; }
        public TimeOnly AdmissionChargeTime { get; set; }
        public decimal FollowUpVisitPercentage { get; set; }
    }
}
