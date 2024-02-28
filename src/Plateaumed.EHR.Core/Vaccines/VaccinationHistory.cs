using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Vaccines;

public class VaccinationHistory : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public long VaccineId { get; set; }
    
    [ForeignKey("VaccineId")]
    public virtual Vaccine Vaccine { get; set; }
    
    public bool HasComplication { get; set; }
    
    public string LastVaccineDuration { get; set; }
    
    public string Note { get; set; }
   
    public int NumberOfDoses { get; set; }

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }
}