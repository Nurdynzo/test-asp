using System;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetInvoiceForCancelQueryResponse
{
    public long Id { get; set; }
    public string InvoiceNo { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public CancelInvoiceItemsQueryResponse[] InvoiceItems { get; set; }
}