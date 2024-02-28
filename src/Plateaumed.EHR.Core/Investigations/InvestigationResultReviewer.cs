using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Investigations
{
    public class InvestigationResultReviewer: FullAuditedEntity<long>, IMustHaveTenant
    {
        public long? InvestigationResultId { get; set; }

        [ForeignKey(nameof(InvestigationResultId))]
        public InvestigationResult InvestigationResult { get; set; }

        public long? ReviewerId { get; set; }

        [ForeignKey("StaffMemberId")]
        public StaffMember Reviewer { get; set; }

        public long? ApproverId { get; set; }       

        public int TenantId { get; set; }

        public long FacilityId { get; set; }

        public long? ElectroRadPulmInvestigationResultId { get; set; }

        [ForeignKey(nameof(ElectroRadPulmInvestigationResultId))]
        public ElectroRadPulmInvestigationResult ElectroRadPulmInvestigationResult { get; set; }
    }
}
