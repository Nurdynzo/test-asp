using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using System.Collections.Generic;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Staff
{
    [Table("JobTitles")]
    [Audited]
    public class JobTitle : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(JobTitleConsts.MaxNameLength, MinimumLength = JobTitleConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [StringLength(
            JobTitleConsts.MaxShortNameLength,
            MinimumLength = JobTitleConsts.MinShortNameLength
        )]
        public virtual string ShortName { get; set; }

        public bool? IsActive { get; set; }

        public bool IsStatic { get; set; }

        public ICollection<JobLevel> JobLevels { get; set; }

        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }

        public long? FacilityId { get; set; }
    }
}
