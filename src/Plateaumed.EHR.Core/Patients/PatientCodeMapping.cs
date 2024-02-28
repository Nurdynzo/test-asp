using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Patients;


public class PatientCodeMapping: FullAuditedEntity<long>
{   
    [ForeignKey("FacilityId")]
    public virtual Facility FacilityFk { get; set; }
    
    public virtual long? FacilityId { get; set; }
    
    [ForeignKey("PatientId")]
    public virtual Patient PatientFk { get; set; }
    
    public virtual long? PatientId { get; set; }
    
    [StringLength(PatientConsts.MaxPatientCodeLength, MinimumLength = PatientConsts.MinPatientCodeLength)]
    public virtual string PatientCode { get; set; } 
}
 