using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.MultiTenancy;

namespace Plateaumed.EHR.MultiTenancy.Dto
{
    public class TenantEditDto : EntityDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(TenantConsts.MaxNameLength)]
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

        [Required]
        public bool HasSignedAgreement { get; set; }

        [DisableAuditing]
        public string ConnectionString { get; set; }

        [Required]
        public int CountryId { get; set; }

        public int? EditionId { get; set; }

        public bool IsActive { get; set; }

        public DateTime? SubscriptionEndDateUtc { get; set; }

        public bool IsInTrialPeriod { get; set; }
    }
}