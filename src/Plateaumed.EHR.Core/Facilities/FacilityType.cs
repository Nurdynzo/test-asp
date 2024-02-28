using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Plateaumed.EHR.Facilities
{
    [Table("FacilityTypes")]
    [Audited]
    public class FacilityType : FullAuditedEntity<long>
    {
        [Required]
        [StringLength(
            FacilityTypeConsts.MaxNameLength,
            MinimumLength = FacilityTypeConsts.MinNameLength
        )]
        public virtual string Name { get; set; }

        public virtual bool IsActive { get; set; }
    }
}
