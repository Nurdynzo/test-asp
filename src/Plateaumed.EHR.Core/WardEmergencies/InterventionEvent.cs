using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.WardEmergencies;

[Table("InterventionEvents")]
public class InterventionEvent: FullAuditedEntity<long>
{
    public string InterventionText { get; set; }

    public long? NursingInterventionId { get; set; }

    [ForeignKey("NursingInterventionId")]
    public NursingIntervention NursingIntervention { get; set; }

    public long PatientInterventionId { get; set; }

    [ForeignKey("PatientInterventionId")]
    public PatientIntervention PatientIntervention { get; set;}
}