using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Investigations
{
    [Table("InvestigationPricing")]
    public class InvestigationPricing : FullAuditedEntity<long>, IMustHaveTenant
    {
		[Required]
		public long InvestigationId { get; set; }

        [ForeignKey(nameof(InvestigationId))]
        public virtual Investigation Investigation { get; set; }

        [Precision(18, 2)]
        public virtual Money Amount { get; set; }

		public bool IsActive { get; set; }

		public string Notes { get; set; }

        public int TenantId { get; set; }
    }
}

