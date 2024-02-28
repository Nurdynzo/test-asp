using Plateaumed.EHR.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Plateaumed.EHR.MultiTenancy
{
    [Table("TenantDocuments")]
    [Audited]
    public class TenantDocument : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual TenantDocumentType Type { get; set; }

        public virtual Guid? Document { get; set; } //File, (BinaryObjectId)

        [StringLength(
            TenantDocumentConsts.MaxFileNameLength,
            MinimumLength = TenantDocumentConsts.MinFileNameLength
        )]
        public virtual string FileName { get; set; }

        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }
    }
}
