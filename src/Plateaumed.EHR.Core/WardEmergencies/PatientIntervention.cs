using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.WardEmergencies;

[Table("PatientInterventions")]
public class PatientIntervention : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public long EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public PatientEncounter Encounter { get; set; }

    public string EventText { get; set; }

    public long? EventId { get; set; }

    [ForeignKey("EventId")]
    public WardEmergency Event { get; set; }

    public List<InterventionEvent> InterventionEvents { get; set; }
}