using Plateaumed.EHR.Insurance;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Insurance.Dtos
{
    public class CreateOrEditInsuranceProviderDto : EntityDto<long?>
    {

        [Required]
        [StringLength(InsuranceProviderConsts.MaxNameLength, MinimumLength = InsuranceProviderConsts.MinNameLength)]
        public string Name { get; set; }
        public bool isActive { get; set; }
        public InsuranceProviderType Type { get; set; }

    }
}