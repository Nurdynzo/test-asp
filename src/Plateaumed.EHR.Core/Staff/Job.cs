using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Organizations;

namespace Plateaumed.EHR.Staff
{
    [Table("Jobs")]
    public class Job : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
       
        public bool IsPrimary { get; set; }

        public long StaffMemberId { get; set; }

        [ForeignKey("StaffMemberId")]
        public StaffMember StaffMember { get; set; }

        public long? JobLevelId { get; set; }

        [ForeignKey("JobLevelId")] 
        public JobLevel JobLevel { get; set; }

        public int? TeamRoleId { get; set; }

        [ForeignKey("TeamRoleId")]
        public Role TeamRole { get; set; }

        public int? JobRoleId { get; set; }

        [ForeignKey("JobRoleId")]
        public Role JobRole { get; set; }

        public long? FacilityId { get; set; }

        [ForeignKey("FacilityId")] 
        public Facility Facility { get; set; }

        public long? UnitId { get; set; }

        [ForeignKey("UnitId")]
        public OrganizationUnitExtended Unit { get; set; }

        public long? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public OrganizationUnitExtended Department { get; set; }

        public ICollection<JobServiceCentre> JobServiceCentres { get; set; } = new List<JobServiceCentre>();

        public ICollection<WardJob> WardsJobs { get; set; } = new List<WardJob>();
    }
}