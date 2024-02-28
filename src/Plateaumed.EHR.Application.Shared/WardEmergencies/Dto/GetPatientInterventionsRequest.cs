using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.WardEmergencies.Dto;

public class GetPatientInterventionsRequest
{
    [Required]
    public long PatientId { get; set; }
}