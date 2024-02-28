using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Misc.Country
{
    public class District : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(DistrictConsts.MaxNameLength, MinimumLength = DistrictConsts.MinNameLength)]
        public virtual string Name {get; set;}

        [Required]
        [ForeignKey("RegionId")]
        public Region RegionFk {get; set;}

        public virtual int RegionId {get; set;}
    }
}