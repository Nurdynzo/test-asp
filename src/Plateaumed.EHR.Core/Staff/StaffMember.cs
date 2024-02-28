using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Roles;

namespace Plateaumed.EHR.Staff
{
    [Table("StaffMembers")]
    [Audited]
    public class StaffMember : FullAuditedEntity<long>
    {
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserFk { get; set; } = null;

        [StringLength(
            StaffMemberConsts.MaxStaffCodeLength,
            MinimumLength = StaffMemberConsts.MinStaffCodeLength
        )]
        public string StaffCode { get; set; }

        public int? AdminRoleId { get; set; }

        [ForeignKey("AdminRoleId")]
        public Role AdminRole { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public ICollection<FacilityStaff> AssignedFacilities { get; set; } = new List<FacilityStaff>();

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
