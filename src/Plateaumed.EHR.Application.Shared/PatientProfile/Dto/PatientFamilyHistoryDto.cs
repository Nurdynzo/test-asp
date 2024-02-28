using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class PatientFamilyHistoryDto : EntityDto<long?>
    {
        [Required]
        public long PatientId { get; set; }
        public bool IsFamilyHistoryKnown { get; set; }
        public int TotalNumberOfSiblings { get; set; }
        public int TotalNumberOfMaleSiblings { get; set; }
        public int TotalNumberOfFemaleSiblings { get; set; }
        public int TotalNumberOfChildren { get; set; }
        public int TotalNumberOfMaleChildren { get; set; }
        public int TotalNumberOfFemaleChildren { get; set; }
        public List<PatientFamilyMembersDto> FamilyMembers { get; set; }
    }
}
