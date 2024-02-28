using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Dto;
public class CreatePhysicalExerciseCommandRequest
{
    public int FrequencyPerWeek { get; set; }
    
    public int DurationPerMinute { get; set; }
    
    public Intensity Intensity { get; set; }
    
    [Required]
    public long PatientId { get; set; }
}