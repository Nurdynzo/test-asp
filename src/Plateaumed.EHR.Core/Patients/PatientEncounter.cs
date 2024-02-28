using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.Organizations;

namespace Plateaumed.EHR.Patients;

[Table("PatientEncounters")]
public class PatientEncounter: FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long PatientId { get; set; }
    
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
    
    public long? AppointmentId { get; set; }
    
    [ForeignKey("AppointmentId")]
    public PatientAppointment Appointment { get; set; }
    
    public DateTime? TimeIn { get; set; }
    
    public DateTime? TimeOut { get; set; }
    
    public EncounterStatusType Status { get; set; }

    public ServiceCentreType ServiceCentre { get; set; }

    public long? UnitId { get; set; }

    [ForeignKey("UnitId")]
    public OrganizationUnitExtended Unit { get; set; }

    public long? FacilityId { get; set; }

    [ForeignKey("FacilityId")]
    public Facility Facility { get; set; }

    public long? AdmissionId { get; set; }

    [ForeignKey("AdmissionId")]
    public Admission Admission { get; set; }

    public long? WardId { get; set; }

    [ForeignKey("WardId")]
    public Ward Ward { get; set; }

    public long? WardBedId { get; set; }

    [ForeignKey("WardBedId")]
    public WardBed WardBed { get; set; }

    public ICollection<StaffEncounter> StaffEncounters { get; set; }

    public ICollection<InvestigationResult> InvestigationResults { get; set; }

    public ICollection<NursingCareSummary> NursingCareSummaries { get; set; }
}
