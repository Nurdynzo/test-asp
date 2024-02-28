using System;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class CreateRecreationalDrugsHistoryRequestDto
    {
        [Required]
        public long PatientId { get; set; }
        public bool PatientDoesNotTakeRecreationalDrugs { get; set; }
        public string DrugUsed { get; set; }
        public string Route { get; set; }
        public bool StillUsingDrugs { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Note { get; set; }
        public long? Id { get; set; }
    }
}