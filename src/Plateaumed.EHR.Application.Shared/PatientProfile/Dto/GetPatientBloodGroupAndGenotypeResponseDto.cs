using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetPatientBloodGroupAndGenotypeResponseDto
    {
        [Required]
        public long PatientId { get; set; }
        public string BloodGroup { get; set; }
        public string Genotype { get; set; }
        public string BloodGroupSource { get; set; }
        public string GenotypeSource { get; set; }
    }
}
