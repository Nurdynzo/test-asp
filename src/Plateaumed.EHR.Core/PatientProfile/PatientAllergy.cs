using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile;

[Table("PatientAllergies")]
public class PatientAllergy : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    [StringLength(PatientAllergyConst.MaxStringLength)]
    public string AllergyType { get; set; }

    public long? AllergySnomedId { get; set; }

    [StringLength(PatientAllergyConst.MaxStringLength)]
    public string Reaction { get; set; }

    public long? ReactionSnomedId { get; set; }
    
    public string Notes { get; set; }

    public Severity Severity { get; set; }
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public string Interval { get; set; }
}