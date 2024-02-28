using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class CreateOrEditRegionDto : EntityDto<int?>
    {
    [Required]
    [StringLength(RegionConsts.MaxNameLength, MinimumLength = RegionConsts.MinNameLength)]
    public string Name { get; set;}

    [StringLength(RegionConsts.MaxShortNameLength, MinimumLength = RegionConsts.MinShortNameLength)]
    public string ShortName { get; set;}

    [Required]
    public string CountryId {get; set;}
    }
}