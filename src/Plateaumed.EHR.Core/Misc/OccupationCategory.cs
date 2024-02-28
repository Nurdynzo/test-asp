using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;

namespace Plateaumed.EHR.Misc
{
    [Table("OccupationCategories")]
    [Audited]
    public class OccupationCategory : AuditedEntity<long>
    {
        [Required]
        [StringLength(OccupationCategoryConsts.MaxNameLength, MinimumLength = OccupationCategoryConsts.MinNameLength)]
        public virtual string Name { get; set; }
    }
}