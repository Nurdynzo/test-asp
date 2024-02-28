using System;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

public class GetInvoicesForRefundQueryRequest
{
    public WalletRefundFilter? Filter { get; set; }
    public DateTime? CustomDate { get; set; }

    [Required]
    public long PatientId { get; set; }
}