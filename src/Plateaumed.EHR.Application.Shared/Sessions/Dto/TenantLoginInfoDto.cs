using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Timing;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.MultiTenancy.Payments;

namespace Plateaumed.EHR.Sessions.Dto
{
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }

        public bool? IsOnboarded { get; set; }

        public int? CountryId { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }

        public TenantOnboardingStatus? OnboardingStatus { get; set; }

        public TenantCategoryType Category { get; set; }

        public TenantType Type { get; set; }

        public virtual Guid? DarkLogoId { get; set; }

        public virtual string DarkLogoFileType { get; set; }
        
        public virtual Guid? LightLogoId { get; set; }

        public virtual string LightLogoFileType { get; set; }

        public Guid? CustomCssId { get; set; }

        public DateTime? SubscriptionEndDateUtc { get; set; }

        public bool IsInTrialPeriod { get; set; }

        public SubscriptionPaymentType SubscriptionPaymentType { get; set; }

        public EditionInfoDto Edition { get; set; }
        
        public List<NameValueDto> FeatureValues { get; set; }

        public DateTime CreationTime { get; set; }

        public PaymentPeriodType PaymentPeriodType { get; set; }

        public string SubscriptionDateString { get; set; }

        public string CreationTimeString { get; set; }

        public TenantLoginInfoDto()
        {
            FeatureValues = new List<NameValueDto>();
        }
        
        public bool IsInTrial()
        {
            return IsInTrialPeriod;
        }

        public bool SubscriptionIsExpiringSoon(int subscriptionExpireNootifyDayCount)
        {
            if (SubscriptionEndDateUtc.HasValue)
            {
                return Clock.Now.ToUniversalTime().AddDays(subscriptionExpireNootifyDayCount) >= SubscriptionEndDateUtc.Value;
            }

            return false;
        }

        public int GetSubscriptionExpiringDayCount()
        {
            if (!SubscriptionEndDateUtc.HasValue)
            {
                return 0;
            }

            return Convert.ToInt32(SubscriptionEndDateUtc.Value.ToUniversalTime().Subtract(Clock.Now.ToUniversalTime()).TotalDays);
        }

        public bool HasRecurringSubscription()
        {
            return SubscriptionPaymentType != SubscriptionPaymentType.Manual;
        }
        
        public virtual bool HasLogo()
        {
            return (DarkLogoId != null && DarkLogoFileType != null) ||
                   (LightLogoId != null && LightLogoFileType != null);
        }
    }
}
