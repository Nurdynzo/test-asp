using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditWardDto : EntityDto<long?>
    {
        [Required]
        public long FacilityId { get; set; }

        [Required]
        [StringLength(WardConsts.MaxNameLength, MinimumLength = WardConsts.MinNameLength)]
        public string Name { get; set; }

        [StringLength(
            WardConsts.MaxDescriptionLength,
            MinimumLength = WardConsts.MinDescriptionLength
        )]

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public List<CreateOrEditWardBedDto> WardBeds { get; set; }
    }
}
