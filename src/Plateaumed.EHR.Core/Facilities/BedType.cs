using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Plateaumed.EHR.Facilities
{
    [Table("BedTypes")]
    [Audited]
    public class BedType : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(BedTypeConsts.MaxNameLength, MinimumLength = BedTypeConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }

        public long? FacilityId { get; set; }
    }
}
