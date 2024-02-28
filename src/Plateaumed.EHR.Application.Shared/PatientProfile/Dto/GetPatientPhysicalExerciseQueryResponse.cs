using System;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Dto;
public class GetPatientPhysicalExerciseQueryResponse
{
    public int FrequencyPerWeek { get; set; }
    
    public int DurationPerMinute { get; set; }
    
    public Intensity Intensity { get; set; }
    
    public long PatientId { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string LastModifiedBy { get; set; }
}