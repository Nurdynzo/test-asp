using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class CreateOrEditDistrictDto : EntityDto<int?>
    {
        [Required]
        [StringLength(DistrictConsts.MaxNameLength, MinimumLength = DistrictConsts.MinNameLength)]
        public string Name {get; set;}

        [Required]
        public string RegionId {get; set;}
    }
}