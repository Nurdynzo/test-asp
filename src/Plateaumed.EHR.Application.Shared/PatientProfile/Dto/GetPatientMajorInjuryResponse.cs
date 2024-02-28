namespace Plateaumed.EHR.PatientProfile.Dto;

public class GetPatientMajorInjuryResponse
{
    public long Id { get; set; }
    
    public string Diagnosis { get; set; }

    public string PeriodOfInjury { get; set; }
    
    public bool IsOngoing { get; set; }
    
    public bool IsComplicationPresent { get; set; }

    public string Notes { get; set; }
}