using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.MultiTenancy.Dto
{
    public class TenantListDto : EntityDto, IPassivable, IHasCreationTime
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }

        public TenantCategoryType Category { get; set; }

        public TenantType Type { get; set; }

        public string EditionDisplayName { get; set; }

        public string Country { get; set; }

        [DisableAuditing]
        public string ConnectionString { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? SubscriptionEndDateUtc { get; set; }

        public int? EditionId { get; set; }

        public bool IsInTrialPeriod { get; set; }
    }
}