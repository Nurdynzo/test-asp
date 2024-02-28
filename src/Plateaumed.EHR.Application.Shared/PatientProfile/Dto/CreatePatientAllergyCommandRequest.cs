using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto;

public class CreatePatientAllergyCommandRequest
{
    public string AllergyType { get; set; }

    public long? AllergySnomedId { get; set; }

    public string Reaction { get; set; }

    public long? ReactionSnomedId { get; set; }
    
    public string Notes { get; set; }

    public Severity Severity { get; set; }
    
    [Required]
    public long PatientId { get; set; }
    
    public string Interval { get; set; }
}