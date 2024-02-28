using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityGroupPatientCodeTemplateDto : EntityDto<long?>
    {
        [Required]
        [StringLength(FacilityGroupConsts.MaxNameLength, MinimumLength = FacilityGroupConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        public List<CreateOrEditFacilityPatientCodeTemplateDto> ChildFacilities { get; set; }
    }
}
