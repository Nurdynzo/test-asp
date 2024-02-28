using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.Timing;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.AllInputs;

namespace Plateaumed.EHR.Authorization.Users
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User : AbpUser<User>
    {
        public virtual Guid? ProfilePictureId { get; set; }

        public virtual bool ShouldChangePasswordOnNextLogin { get; set; }

        public DateTime? SignInTokenExpireTimeUtc { get; set; }

        public string SignInToken { get; set; }

        public string GoogleAuthenticatorKey { get; set; }

        public string RecoveryCode { get; set; }

        public List<UserOrganizationUnit> OrganizationUnits { get; set; }

        //Can add application specific user properties here

        public TitleType? Title { get; set; }

        public string MiddleName { get; set; }

        [EmailAddress]
        public string AltEmailAddress { get; set; }

        public GenderType? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(UserConsts.MaxIdentificationCodeLength, MinimumLength = UserConsts.MinIdentificationCodeLength)]
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

        [StringLength(UserConsts.MaxPostCodeLength, MinimumLength = UserConsts.MinPostCodeLength)]
        public string PostCode { get; set; }

        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public StaffMember StaffMemberFk { get; set; } 

        public Patient PatientFk { get; set; }

        [NotMapped]
        public string DisplayName => (Title.HasValue ? Title + " " : string.Empty) + Name + " " + (!MiddleName.IsNullOrEmpty() ? MiddleName.Substring(0, 1) + ". " : string.Empty) + Surname;

        [NotMapped]
        public override string FullName => Name + " " + (!MiddleName.IsNullOrEmpty() ? MiddleName + " " : string.Empty) + Surname;

        public User()
        {
            IsLockoutEnabled = true;
            IsTwoFactorEnabled = true;
        }

        /// <summary>
        /// Creates admin <see cref="User"/> for a tenant.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="emailAddress">Email address</param>
        /// <param name="userName">Username of admin user</param>
        /// <param name="name">Name of admin user</param>
        /// <param name="surname">Surname of admin user</param>
        /// <returns>Created <see cref="User"/> object</returns>
        public static User CreateTenantAdminUser(int tenantId, string userName, string emailAddress, string name = null, string surname = null)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = string.IsNullOrWhiteSpace(userName) ? AdminUserName : userName,
                Name = string.IsNullOrWhiteSpace(name) ? AdminUserName : name,
                Surname = string.IsNullOrWhiteSpace(surname) ? AdminUserName : surname,
                EmailAddress = emailAddress,
                StaffMemberFk = new StaffMember(),
                Roles = new List<UserRole>(),
                OrganizationUnits = new List<UserOrganizationUnit>()
            };

            user.SetNormalizedNames();

            return user;
        }

        public override void SetNewPasswordResetCode()
        {
            /* This reset code is intentionally kept short.
             * It should be short and easy to enter in a mobile application, where user can not click a link.
             */
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(10).ToUpperInvariant();
        }

        public void Unlock()
        {
            AccessFailedCount = 0;
            LockoutEndDateUtc = null;
        }

        public void SetSignInToken()
        {
            SignInToken = Guid.NewGuid().ToString();
            SignInTokenExpireTimeUtc = Clock.Now.AddMinutes(1).ToUniversalTime();
        }
    }
}