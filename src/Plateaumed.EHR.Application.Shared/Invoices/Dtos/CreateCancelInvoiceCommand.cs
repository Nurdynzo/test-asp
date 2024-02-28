using System;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.Invoices.Dtos;

public class CreateCancelInvoiceCommand
{
  
    [Required]
    public long PatientId { get; set; }
    
    public long[] InvoiceItemsIds { get; set; }
}