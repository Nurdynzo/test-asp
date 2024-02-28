using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Patients;

[Table("Admissions")]
public class Admission : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long? AttendingPhysicianId { get; set; }

    [ForeignKey("AttendingPhysicianId")]
    public StaffMember AttendingPhysician { get; set; }
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public long FacilityId { get; set; }

    [ForeignKey("FacilityId")]
    public Facility Facility { get; set; }

    public long? AdmittingEncounterId { get; set; }

    [ForeignKey("AdmittingEncounterId")]
    public PatientEncounter AdmittingEncounter { get; set; }
}