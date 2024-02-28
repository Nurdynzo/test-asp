using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Staff
{
    [Serializable]
    [Table("JobLevels")]
    [Audited]
    public class JobLevel : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(JobLevelConsts.MaxNameLength, MinimumLength = JobLevelConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Range(JobLevelConsts.MinRankValue, JobLevelConsts.MaxRankValue)]
        public virtual int Rank { get; set; }

        [StringLength(
            JobLevelConsts.MaxShortNameLength,
            MinimumLength = JobLevelConsts.MinShortNameLength
        )]
        public virtual string ShortName { get; set; }

        public bool? IsActive { get; set; }

        public bool IsStatic { get; set; }

        public virtual long JobTitleId { get; set; }

        [ForeignKey("JobTitleId")]
        public JobTitle JobTitleFk { get; set; }

        public TitleType? TitleOfAddress { get; set; }
    }
}
