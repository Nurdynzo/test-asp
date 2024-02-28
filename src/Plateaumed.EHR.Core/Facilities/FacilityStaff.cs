using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Plateaumed.EHR.Facilities
{
    [Table("FacilityStaff")]
    [Audited]
    public class FacilityStaff : CreationAuditedEntity<long>
    {
        public bool IsDefault { get; set; }

        public virtual long FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public Facility FacilityFk { get; set; }

        public virtual long StaffMemberId { get; set; }

        [ForeignKey("StaffMemberId")]
        public StaffMember StaffMemberFk { get; set; }

    }
}