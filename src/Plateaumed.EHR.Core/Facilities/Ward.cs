using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities
{
    [Table("Wards")]
    [Audited]
    public class Ward : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(WardConsts.MaxNameLength, MinimumLength = WardConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [StringLength(
            WardConsts.MaxDescriptionLength,
            MinimumLength = WardConsts.MinDescriptionLength
        )]
        public virtual string Description { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual long FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public Facility FacilityFk { get; set; }

        public virtual ICollection<WardBed> WardBeds { get; set; } = new List<WardBed>();
    }
}
