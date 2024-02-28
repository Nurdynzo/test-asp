using System;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

public class RefundInvoiceQueryResponse
{
    public long Id { get; set; }
    public string InvoiceNo { get; set; }
    public DateTimeOffset PaymentDate { get; set; }

    public decimal PercentageToBeDeducted { get; set; }
    public RefundInvoiceItemsQueryResponse[] InvoiceItems { get; set; }
}