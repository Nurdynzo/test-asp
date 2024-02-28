using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;

namespace Plateaumed.EHR.Staff
{
    [Table("StaffCodeTemplates")]
    [Audited]
    public class StaffCodeTemplate : FullAuditedEntity<long>
    {
        public virtual int Length { get; set; }

        public virtual int StartingIndex { get; set; }

        [StringLength(
            StaffCodeTemplateConsts.MaxPrefixLength,
            MinimumLength = StaffCodeTemplateConsts.MinPrefixLength
        )]
        public virtual string Prefix { get; set; }

        [StringLength(
            StaffCodeTemplateConsts.MaxSuffixLength,
            MinimumLength = StaffCodeTemplateConsts.MinSuffixLength
        )]
        public virtual string Suffix { get; set; }
    }
}
