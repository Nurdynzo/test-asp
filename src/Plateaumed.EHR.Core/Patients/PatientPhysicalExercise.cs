using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Patients;

[Table("PatientPhysicalExercises")]
public class PatientPhysicalExercise : FullAuditedEntity<long>
{
    public int FrequencyPerWeek { get; set; }
    
    public int DurationPerMinute { get; set; }
    
    public Intensity Intensity { get; set; }
    
    public long PatientId { get; set; }
    
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
    
}