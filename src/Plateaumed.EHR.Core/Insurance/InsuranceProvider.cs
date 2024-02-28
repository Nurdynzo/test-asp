using Plateaumed.EHR.Insurance;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Plateaumed.EHR.Misc.Country;

namespace Plateaumed.EHR.Insurance
{
    [Table("InsuranceProviders")]
    [Audited]
    public class InsuranceProvider : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(InsuranceProviderConsts.MaxNameLength, MinimumLength = InsuranceProviderConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public bool isActive { get; set; }
        public virtual InsuranceProviderType Type { get; set; }

        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }
    }
}