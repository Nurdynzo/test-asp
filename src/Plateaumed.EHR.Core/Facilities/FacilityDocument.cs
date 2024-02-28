using Plateaumed.EHR.Facilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Plateaumed.EHR.Facilities
{
    [Table("FacilityDocuments")]
    [Audited]
    public class FacilityDocument : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [StringLength(
            FacilityDocumentConsts.MaxFileNameLength,
            MinimumLength = FacilityDocumentConsts.MinFileNameLength
        )]
        public virtual string FileName { get; set; }

        [Required]
        [StringLength(
            FacilityDocumentConsts.MaxDocumentTypeLength,
            MinimumLength = FacilityDocumentConsts.MinDocumentTypeLength
        )]
        public virtual string DocumentType { get; set; }

        //File

        public virtual Guid? Document { get; set; } //File, (BinaryObjectId)

        public virtual long? FacilityGroupId { get; set; }

        [ForeignKey("FacilityGroupId")]
        public FacilityGroup FacilityGroupFk { get; set; }

        public virtual long? FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public Facility FacilityFk { get; set; }
    }
}
