using System;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class CreatePatientImplantCommandRequestDto
    {
        [Required]
        public long PatientId { get; set; }
        public string Name { get; set; }
        public long? SnomedId { get; set; }
        public bool IsIntact { get; set; }
        public bool HasComplications { get; set; }
        public string Note { get; set; }
        public DateTime DateInserted { get; set; }
        public DateTime DateRemoved { get; set; }
        public long? Id { get; set; }
    }
}
