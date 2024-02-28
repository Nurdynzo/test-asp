using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityTypeDto : EntityDto<long?>
    {
        [Required]
        [StringLength(
            FacilityTypeConsts.MaxNameLength,
            MinimumLength = FacilityTypeConsts.MinNameLength
        )]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
