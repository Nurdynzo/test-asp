using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Patients
{
    [Table("PatientCodeTemplates")]
    [Audited]
    public class PatientCodeTemplate : FullAuditedEntity<long>
    {
        [StringLength(PatientCodeTemplateConsts.MaxPrefixLength, MinimumLength = PatientCodeTemplateConsts.MinPrefixLength)]
        public virtual string Prefix { get; set; }

        public virtual int Length { get; set; }

        [StringLength(PatientCodeTemplateConsts.MaxSuffixLength, MinimumLength = PatientCodeTemplateConsts.MinSuffixLength)]
        public virtual string Suffix { get; set; }

        public virtual int StartingIndex { get; set; }

        public virtual bool IsActive { get; set; }
    }
}