using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

public class ProcessRefundRequestCommand
{
    [Required]
    public long PatientId { get; set; }

    [Required]
    public MoneyDto TotalAmountToRefund { get; set; }
    
    [Required]
    public bool IsApproved { get; set; }
}