using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CreateOrEditPatientOccupationDto : EntityDto<long?>
    {
        [Required]
        [StringLength(
            PatientOccupationConsts.MaxNameLength,
            MinimumLength = PatientOccupationConsts.MinNameLength
        )]
        public string Name { get; set; }

        public long PatientOccupationCategoryId { get; set; }

        public bool IsCurrent { get; set; }
    }
}
