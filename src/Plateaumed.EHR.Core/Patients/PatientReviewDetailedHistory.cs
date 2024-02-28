using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Patients;

[Table("PatientReviewDetailedHistories")]

public class PatientReviewDetailedHistory : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public long EncounterId { get; set; }
    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }

    [ForeignKey("CreatorUserId")]
    public User UserFk { get; set; } = null;
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Note { get; set; }
    public bool IsAutoSaved { get; set; }
}
