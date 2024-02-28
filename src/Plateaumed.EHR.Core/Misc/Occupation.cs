using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;

namespace Plateaumed.EHR.Misc
{
    [Table("Occupations")]
    [Audited]
    public class Occupation : AuditedEntity<long>
    {
        [Required]
        [StringLength(OccupationCategoryConsts.MaxNameLength, MinimumLength = OccupationCategoryConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public long OccupationCategoryId { get; set; }

        [ForeignKey("OccupationCategoryId")]
        public OccupationCategory OccupationCategory { get; set; }
    }
}