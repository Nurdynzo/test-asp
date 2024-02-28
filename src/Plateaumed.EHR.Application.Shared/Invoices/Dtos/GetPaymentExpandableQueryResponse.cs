using System;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetPaymentExpandableQueryResponse
{
    public string  InvoiceNo { get; set; }
    
    public string InvoiceItemName { get; set; }
    
    public string AppointmentStatus { get; set; }
    
    public MoneyDto TopUpMoney { get; set; }

    public MoneyDto ActualInvoiceAmount { get; set; }

    public MoneyDto AmountPaid { get; set; }

    public DateTime? LastPaidDateTime { get; set; }

    public MoneyDto OutstandingAmount { get; set; }
    

    public MoneyDto EditedAmount { get; set; }

    public string PaymentType { get; set; }
    
    public DateTime? InvoiceItemDateTime { get; set; }

    public string EditorInfo { get; set; }
}