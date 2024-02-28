using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.Authorization.Users.Dto
{
    //Mapped to/from User in CustomDtoMapper
    public class UserEditDto : IPassivable
    {
        /// <summary>
        /// Set null to create a new user. Set user's Id to update a user
        /// </summary>
        public long? Id { get; set; }

        [Required]
        public TitleType? Title { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        public string MiddleName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string FullName { get; set; }

        public GenderType? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string IdentificationCode { get; set; }

        public IdentificationType? IdentificationType { get; set; }

        [StringLength(UserConsts.MaxStreetAddressLength, MinimumLength = UserConsts.MinStreetAddressLength)]
        public string StreetAddress { get; set; }

        [StringLength(UserConsts.MaxCityLength, MinimumLength = UserConsts.MinCityLength)]
        public string City { get; set; }

        [StringLength(UserConsts.MaxDistrictLength, MinimumLength = UserConsts.MinDistrictLength)]
        public string District { get; set; }

        [StringLength(UserConsts.MaxStateLength, MinimumLength = UserConsts.MinStateLength)]
        public string State { get; set; }

        public string PostCode { get; set; }

        public int? CountryId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        
        public string AltEmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        // Not used "Required" attribute since empty value is used to 'not change password'
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool ShouldChangePasswordOnNextLogin { get; set; }

        public virtual bool IsTwoFactorEnabled { get; set; }

        public virtual bool IsLockoutEnabled { get; set; }

    }
}