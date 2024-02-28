using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Insurance;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Plateaumed.EHR.Facilities
{
    [Table("FacilityInsurers")]
    [Audited]
    public class FacilityInsurer : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual long? FacilityGroupId { get; set; }

        [ForeignKey("FacilityGroupId")]
        public FacilityGroup FacilityGroupFk { get; set; }

        public virtual long? FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public Facility FacilityFk { get; set; }

        public virtual long InsuranceProviderId { get; set; }

        [ForeignKey("InsuranceProviderId")]
        public InsuranceProvider InsuranceProviderFk { get; set; }

    }
}