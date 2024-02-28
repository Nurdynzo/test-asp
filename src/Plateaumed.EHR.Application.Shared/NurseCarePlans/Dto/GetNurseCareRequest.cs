using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.NurseCarePlans.Dto;

public class GetNurseCareRequest
{
    [Required]
    public long PatientId { get; set; }
    
    [Required]
    public long EncounterId { get; set; }
}