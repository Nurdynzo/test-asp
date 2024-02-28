using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Patients;

[Table("StaffEncounters")]
public class StaffEncounter: CreationAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long StaffId { get; set; }

    [ForeignKey("StaffId")]
    public StaffMember Staff { get; set; }

    public long EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public PatientEncounter Encounter { get; set; }
}