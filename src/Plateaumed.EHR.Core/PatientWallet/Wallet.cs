using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.PatientWallet;

[Table("Wallets")]
public class Wallet: FullAuditedEntity<long>, IMayHaveTenant
{
    [StringLength(WalletConstants.MaxAccountNumberLength)]
    public virtual string AccountNumber { get; set; }
    
    public virtual long PatientId { get; set; }
    
    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }

    [Precision(18,2)]
    public virtual Money Balance { get; set; }
    
    [StringLength(WalletConstants.MaxNameLength)]
    public virtual string  BankName { get; set; }
    public virtual ICollection<WalletHistory> History { get; set; }
    
    public int? TenantId { get; set; }
    
    [Timestamp]
    public byte[] RowVersion { get; set; }
}