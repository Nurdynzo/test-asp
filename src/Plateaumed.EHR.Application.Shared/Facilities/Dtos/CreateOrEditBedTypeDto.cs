using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditBedTypeDto : EntityDto<long?>
    {
        [Required]
        [StringLength(BedTypeConsts.MaxNameLength, MinimumLength = BedTypeConsts.MinNameLength)]
        public string Name { get; set; }
    }
}
