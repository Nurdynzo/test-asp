using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.AllInputs;

public class Medication : FullAuditedEntity<long>
{
    public int? TenantId { get; set; }
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductSource { get; set; }
    public string DoseUnit { get; set; }
    public string Frequency { get; set; }
    public string Duration { get; set; }
    public string Direction { get; set; }
    public string Note { get; set; }
    public long? ProcedureId { get; set; }
    
    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }
    public bool IsAdministered { get; set; }
    public bool IsDiscontinued { get; set; }
    public long? DiscontinueUserId { get; set; }
    [ForeignKey("DiscontinueUserId")]
    public User User { get; set; }
    public ICollection<MedicationAdministrationActivity> MedicationAdministrationActivities { get; set; }
}
