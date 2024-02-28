using System;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class CreateOccupationalHistoryDto
    {
        public string WorkLocation { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Occupation { get; set; }
        public string Note { get; set; }

        [Required]
        public long PatientId { get; set; }
        public bool IsCurrent { get; set; }
        public long? Id { get; set; }
    }
}
