using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityPatientCodeTemplateDto : EntityDto<long?>
    {
        [Required]
        [StringLength(FacilityConsts.MaxNameLength, MinimumLength = FacilityConsts.MinNameLength)]
        public string Name { get; set; }

        public bool? HasPharmacy { get; set; }

        public bool? HasLaboratory { get; set; }

        public CreateOrEditPatientCodeTemplateDto PatientCodeTemplate { get; set; }

        public long GroupId { get; set; }
    }
}
