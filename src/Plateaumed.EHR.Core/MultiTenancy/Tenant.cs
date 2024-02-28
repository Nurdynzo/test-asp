using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.MultiTenancy;
using Abp.Timing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.MultiTenancy.Payments;

namespace Plateaumed.EHR.MultiTenancy
{
    /// <summary>
    /// Represents a Tenant in the system.
    /// A tenant is a isolated customer for the application
    /// which has it's own users, roles and other application entities.
    /// </summary>
    public class Tenant : AbpTenant<User>
    {
        public const int MaxLogoMimeTypeLength = 64;

        //Can add application specific tenant properties here
        public bool? IsOnboarded { get; set; }

        public ICollection<TenantOnboardingProgress> OnboardingProgress { get; set; }

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

        public DateTime? SubscriptionEndDateUtc { get; set; }

        public bool IsInTrialPeriod { get; set; }

        public virtual Guid? CustomCssId { get; set; }

        public virtual Guid? DarkLogoId { get; set; }

        [MaxLength(MaxLogoMimeTypeLength)]
        public virtual string DarkLogoFileType { get; set; }

        public virtual Guid? LightLogoId { get; set; }

        [MaxLength(MaxLogoMimeTypeLength)]
        public virtual string LightLogoFileType { get; set; }

        public SubscriptionPaymentType SubscriptionPaymentType { get; set; }

        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country CountryFk { get; set; }

        public virtual ICollection<TenantDocument> TenantDocuments { get; set; }

        protected Tenant() { }

        public Tenant(string tenancyName, string name, TenantType tenantType) : base(tenancyName, name)
        {
            Type = tenantType;

            switch (tenantType)
            {
                case TenantType.Individual:
                    IsOnboarded = true;
                    break;
                case TenantType.Business:
                    IsOnboarded = false;
                    OnboardingProgress = new List<TenantOnboardingProgress>();
                    break;
            }
        }

        public virtual bool HasLogo()
        {
            return (DarkLogoId != null && DarkLogoFileType != null)
                || (LightLogoId != null && LightLogoFileType != null);
        }

        public virtual bool HasDarkLogo()
        {
            return DarkLogoId != null && DarkLogoFileType != null;
        }

        public virtual bool HasLightLogo()
        {
            return LightLogoId != null && LightLogoFileType != null;
        }

        public void ClearLogo()
        {
            DarkLogoId = null;
            DarkLogoFileType = null;
        }

        public void UpdateSubscriptionDateForPayment(
            PaymentPeriodType paymentPeriodType,
            EditionPaymentType editionPaymentType
        )
        {
            switch (editionPaymentType)
            {
                case EditionPaymentType.NewRegistration:
                case EditionPaymentType.BuyNow:
                    {
                        SubscriptionEndDateUtc = Clock.Now
                            .ToUniversalTime()
                            .AddDays((int)paymentPeriodType);
                        break;
                    }
                case EditionPaymentType.Extend:
                    ExtendSubscriptionDate(paymentPeriodType);
                    break;
                case EditionPaymentType.Upgrade:
                    if (HasUnlimitedTimeSubscription())
                    {
                        SubscriptionEndDateUtc = Clock.Now
                            .ToUniversalTime()
                            .AddDays((int)paymentPeriodType);
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private void ExtendSubscriptionDate(PaymentPeriodType paymentPeriodType)
        {
            if (SubscriptionEndDateUtc == null)
            {
                throw new InvalidOperationException(
                    "Can not extend subscription date while it's null!"
                );
            }

            if (IsSubscriptionEnded())
            {
                SubscriptionEndDateUtc = Clock.Now.ToUniversalTime();
            }

            SubscriptionEndDateUtc = SubscriptionEndDateUtc.Value.AddDays((int)paymentPeriodType);
        }

        private bool IsSubscriptionEnded()
        {
            return SubscriptionEndDateUtc < Clock.Now.ToUniversalTime();
        }

        public int CalculateRemainingHoursCount()
        {
            return SubscriptionEndDateUtc != null
                ? (int)(SubscriptionEndDateUtc.Value - Clock.Now.ToUniversalTime()).TotalHours //converting it to int is not a problem since max value ((DateTime.MaxValue - DateTime.MinValue).TotalHours = 87649416) is in range of integer.
                : 0;
        }

        public bool HasUnlimitedTimeSubscription()
        {
            return SubscriptionEndDateUtc == null;
        }
    }
}
