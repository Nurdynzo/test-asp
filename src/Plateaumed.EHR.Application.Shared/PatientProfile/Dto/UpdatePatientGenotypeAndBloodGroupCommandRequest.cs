using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Dto;
public class UpdatePatientGenotypeAndBloodGroupCommandRequest
{
    [Required]
    public BloodGroup BloodGroup { get; set; }

    [Required]
    public BloodGenotype BloodGenotype { get; set; }
    
    [Required]
    public BloodGroupAndGenotypeSource BloodGroupSource { get; set; }
        
    [Required]
    public BloodGroupAndGenotypeSource GenotypeSource { get; set; }

    [Required]
    public long PatientId { get; set; }
}