using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Invoices;

public class PaymentActivityLog : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    [StringLength(InvoiceConsts.MaxInvoiceIdLength)]
    public string InvoiceNo { get; set; }
    
    [StringLength(InvoiceConsts.MaxNarrationLength)]
    public string Narration { get; set; }

    public Money ToUpAmount { get; set; }

    public Money ActualAmount { get; set; }

    public Money EditAmount { get; set; }
    
    public Money AmountRefund { get; set; }

    public Money AmountPaid { get; set; }
    public Money OutStandingAmount { get; set; }
    public Money ReliefAmount { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public long? PatientId { get; set; }

    public Facility Facility { get; set; }

    public long? FacilityId { get; set; }

    public Invoice Invoice { get; set; }

    public long? InvoiceId { get; set; }

    public long? InvoiceItemId { get; set; }

    [ForeignKey("InvoiceItemId")]
    public InvoiceItem InvoiceItem { get; set; }

    public TransactionType TransactionType { get; set; }

    public TransactionAction TransactionAction { get; set; }
}