using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityGroupDto : EntityDto<long?>
    {
        [Required]
        [StringLength(FacilityGroupConsts.MaxNameLength, MinimumLength = FacilityGroupConsts.MinNameLength)]
        public string Name { get; set; }

        public TenantCategoryType Category { get; set; }

        [Required]
        [StringLength(FacilityGroupConsts.MaxEmailAddressLength, MinimumLength = FacilityGroupConsts.MinEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(FacilityGroupConsts.MaxPhoneNumberLength, MinimumLength = FacilityGroupConsts.MinPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(FacilityGroupConsts.MaxAddressLength, MinimumLength = FacilityGroupConsts.MinAddressLength)]
        public string Address { get; set; }

        [Required]
        [StringLength(FacilityGroupConsts.MaxCityLength, MinimumLength = FacilityGroupConsts.MinCityLength)]
        public string City { get; set; }

        [Required]
        [StringLength(FacilityGroupConsts.MaxStateLength, MinimumLength = FacilityGroupConsts.MinStateLength)]
        public string State { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxNameLength, MinimumLength = CountryConsts.MinNameLength)]
        public string Country { get; set; }

        [StringLength(FacilityGroupConsts.MaxWebsiteLength, MinimumLength = FacilityGroupConsts.MinWebsiteLength)]
        public string Website { get; set; }

        [StringLength(FacilityGroupConsts.MaxPostCodeLength, MinimumLength = FacilityGroupConsts.MinPostCodeLength)]
        public string PostCode { get; set; }

        public List<CreateOrEditFacilityDto> ChildFacilities { get; set; }

        public Guid? LogoId { get; set; }
    }
}
