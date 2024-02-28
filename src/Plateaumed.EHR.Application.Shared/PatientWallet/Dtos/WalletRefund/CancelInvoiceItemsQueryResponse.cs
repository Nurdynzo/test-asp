using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

public class CancelInvoiceItemsQueryResponse
{
    public long Id { get; set; }
    public string ItemName { get; set; }
    public MoneyDto SubTotal { get; set; }
   
}