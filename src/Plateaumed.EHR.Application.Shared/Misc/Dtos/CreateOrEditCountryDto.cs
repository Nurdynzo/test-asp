using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class CreateOrEditCountryDto : EntityDto<int?>
    {
        [Required]
        [StringLength(CountryConsts.MaxNameLength, MinimumLength = CountryConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            CountryConsts.MaxNationalityLength,
            MinimumLength = CountryConsts.MinNationalityLength
        )]
        public string Nationality { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxCodeLength, MinimumLength = CountryConsts.MinCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(
            CountryConsts.MaxPhoneCodeLength,
            MinimumLength = CountryConsts.MinPhoneCodeLength
        )]
        public string PhoneCode { get; set; }
    }
}
