using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.PatientWallet;

[Table("WalletHistories")]
public class WalletHistory : FullAuditedEntity<long>, IMayHaveTenant
{
    [Precision(18, 2)]
    public virtual Money Amount { get; set; }

    public virtual TransactionType TransactionType { get; set; }

    [Precision(18, 2)]
    public virtual Money CurrentBalance { get; set; }
    
    [ForeignKey("WalletId")]
    public virtual Wallet Wallet { get; set; }

    [StringLength(WalletConstants.MaxNarrationLength)]
    public string Narration { get; set; }

    public virtual long WalletId { get; set; }

    public TransactionSource Source { get; set; }

    public virtual long? PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public virtual long? FacilityId { get; set; }
    
    [ForeignKey("FacilityId")]
    public Facility Facility { get; set; }

    public TransactionStatus Status { get; set; }
    
    public int? TenantId { get; set; }
}