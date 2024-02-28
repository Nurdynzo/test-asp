using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.MultiTenancy.Payments;
using Plateaumed.EHR.MultiTenancy.Payments.Dto;

namespace Plateaumed.EHR.MultiTenancy.Dto
{
    public class RegisterTenantInput
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public TenantCategoryType Category { get; set; }

        [Required]
        public TenantType Type { get; set; }
         
        [StringLength(TenantConsts.MaxIndividualSpecializationLength, MinimumLength = TenantConsts.MinIndividualSpecializationLength)]
        public string IndividualSpecialization { get; set; }

        [StringLength(TenantConsts.MaxIndividualGraduatingSchoolLength, MinimumLength = TenantConsts.MinIndividualGraduatingSchoolLength)]
        public string IndividualGraduatingSchool { get; set; }

        [StringLength(TenantConsts.MaxIndividualGraduatingYearLength, MinimumLength = TenantConsts.MinIndividualGraduatingYearLength)] 
        public string IndividualGraduatingYear { get; set; }

        [StringLength(FacilityGroupConsts.MaxNameLength, MinimumLength = FacilityGroupConsts.MinNameLength)]
        public string FacilityGroupName { get; set; }

        [StringLength(FacilityGroupConsts.MaxIndividualDocumentTokenLength)]
        public string IndividualDocumentToken { get; set; }

        public TenantDocumentType? IndividualDocumentType { get; set; }

        
        public int? CountryId { get; set; }

        [Required]
        public bool HasSignedAgreement { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        [StringLength(AbpUserBase.MaxNameLength)]
        public string AdminName { get; set; }

        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string AdminSurname { get; set; }

        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string AdminPassword { get; set; }

        [DisableAuditing]
        public string CaptchaResponse { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public int? EditionId { get; set; }
    }
}
