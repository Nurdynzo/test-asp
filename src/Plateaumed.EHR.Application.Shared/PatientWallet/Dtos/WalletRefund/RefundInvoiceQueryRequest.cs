using System;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

public class RefundInvoiceQueryRequest
{
    [Required]
    public long[] InvoiceIds { get; set; }
    
    [Required]
    public long PatientId { get; set; }

    public WalletRefundFilter? DateFilter { get; set; }

    public  DateTimeOffset? CustomDate { get; set; }
}