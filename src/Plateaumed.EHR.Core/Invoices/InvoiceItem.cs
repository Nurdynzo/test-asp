using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Plateaumed.EHR.ValueObjects;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Invoices
{
    [Table("InvoiceItems")]
    [Audited]
    public class InvoiceItem : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(InvoiceItemConsts.MaxNameLength, MinimumLength = InvoiceItemConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual int Quantity { get; set; }

        public virtual Money UnitPrice { get; set; }

        public virtual Money DiscountAmount { get; set; }

        [Range(InvoiceItemConsts.MinDiscountPercentageValue, InvoiceItemConsts.MaxDiscountPercentageValue)]
        public virtual decimal? DiscountPercentage { get; set; }
        
        public virtual Money SubTotal { get; set; }

        [StringLength(InvoiceItemConsts.MaxNotesLength, MinimumLength = InvoiceItemConsts.MinNotesLength)]
        public virtual string Notes { get; set; }

        public virtual long? InvoiceId { get; set; }

        [ForeignKey("InvoiceId")]
        public  Invoice InvoiceFk { get; set; }
        
        public Money OutstandingAmount { get; set; }
        public virtual Money AmountPaid { get; set; }
        public Money DebtReliefAmount { get; set; }
        public virtual bool IsReliefApplied { get; set; }

        public long? FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }

        public InvoiceItemStatus Status { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}