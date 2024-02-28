using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Invoices;

public class InvoiceCancelRequest : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public virtual long? PatientId { get; set; }

    public Patient Patient { get; set; }

    public long? FacilityId { get; set; }

    [ForeignKey("FacilityId")]
    public Facility Facility { get; set; }
    
    public long? InvoiceId { get; set; }

    [ForeignKey("InvoiceId")]
    public Invoice Invoice { get; set; }

    public long? InvoiceItemId { get; set; }

    [ForeignKey("InvoiceItemId")]
    public InvoiceItem InvoiceItem { get; set; }

    public InvoiceCancelStatus Status { get; set; }
}