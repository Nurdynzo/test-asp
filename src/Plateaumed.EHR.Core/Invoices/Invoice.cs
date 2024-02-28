using Plateaumed.EHR.Patients;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using System.Collections.Generic;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.ValueObjects;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Invoices
{
    [Table("Invoices")]
    [Audited]
    public class Invoice : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        [StringLength(InvoiceConsts.MaxInvoiceIdLength, MinimumLength = InvoiceConsts.MinInvoiceIdLength)]
        public virtual string InvoiceId { get; set; }
        public virtual DateTime TimeOfInvoicePaid { get; set; }
        public virtual long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }
        public virtual long? PatientAppointmentId { get; set; }
        [ForeignKey("PatientAppointmentId")]
        public PatientAppointment PatientAppointmentFk { get; set; }
        [ForeignKey("PatientEncounterId")]
        public PatientEncounter PatientEncounter { get; set; }
        public long? PatientEncounterId { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public InvoiceSource InvoiceSource { get; set; }
        public bool IsEdited { get; set; }
        [Required]
        public Money TotalAmount { set; get; }
        public Money OutstandingAmount { get; set; }
        public virtual Money AmountPaid { get; set; }
        public Money DiscountPercentage { get; set; }
        public bool IsServiceOnCredit { get; set; }
        public long? FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
