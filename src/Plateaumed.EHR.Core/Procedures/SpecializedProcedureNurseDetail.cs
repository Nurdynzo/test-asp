using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Staff;
namespace Plateaumed.EHR.Procedures
{
    public class SpecializedProcedureNurseDetail : FullAuditedEntity<long> , IMustHaveTenant
    {

        public int TenantId { get; set; }
        public TimeOnly TimePatientReceived { get; set; }
        public long? ScrubStaffMemberId { get; set; }
        [ForeignKey("ScrubStaffMemberId")]
        public StaffMember ScrubStaffMember { get; set; }
        public long? CirculatingStaffMemberId { get; set; }
        [ForeignKey("CirculatingStaffMemberId")]
        public StaffMember CirculatingStaffMember { get; set; }
        public long ProcedureId { get; set; }
        [ForeignKey("ProcedureId")]
        public Procedure Procedure { get; set; }

    }
}
