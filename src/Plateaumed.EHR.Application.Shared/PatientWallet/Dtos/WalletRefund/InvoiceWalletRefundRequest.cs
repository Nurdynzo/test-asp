using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

public class InvoiceWalletRefundRequest
{
    [Required]
    public long PatientId { get; set; }

    public long[] InvoiceItemsIds { get; set; }
    
    
}