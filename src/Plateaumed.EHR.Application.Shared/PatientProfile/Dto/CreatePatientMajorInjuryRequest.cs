using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.PatientProfile.Dto;

public class CreatePatientMajorInjuryRequest
{
    public string Diagnosis { get; set; }

    public int  PeriodOfInjury { get; set; }

    public UnitOfTime PeriodOfInjuryUnit { get; set; }

    public bool IsOngoing { get; set; }

    public string Notes { get; set; }

    public bool IsComplicationPresent { get; set; }
    
    [Required]
    public long PatientId { get; set; }
    public long? Id { get; set; }
}