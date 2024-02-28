using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityDto : EntityDto<long?>
    {
        [Required]
        [StringLength(FacilityConsts.MaxNameLength, MinimumLength = FacilityConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public FacilityLevel? Level { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxEmailAddressLength, MinimumLength = FacilityConsts.MinEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxPhoneNumberLength, MinimumLength = FacilityConsts.MinPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxAddressLength, MinimumLength = FacilityConsts.MinAddressLength)]
        public string Address { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxCityLength, MinimumLength = FacilityConsts.MinCityLength)]
        public string City { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxStateLength, MinimumLength = FacilityConsts.MinStateLength)]
        public string State { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxNameLength, MinimumLength = CountryConsts.MinNameLength)]
        public string Country { get; set; }

        [StringLength(FacilityConsts.MaxWebsiteLength, MinimumLength = FacilityConsts.MinWebsiteLength)]
        public string Website { get; set; }

        [StringLength(FacilityConsts.MaxPostCodeLength, MinimumLength = FacilityConsts.MinPostCodeLength)]
        public string PostCode { get; set; }

        public long TypeId { get; set; }

        public long GroupId { get; set; }

        public Guid? LogoId { get; set; }
    }
}
