using System;
using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetPaymentLadingListQueryResponse
{
    public long PatientId { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string MiddleName { get; set; }
    public string PatientCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public MoneyDto WalletBalance { get; set; }
    public string Ward { get; set; }
    public string AppointmentStatus { get; set; }
    public MoneyDto WalletTopUpAmount { get; set; }
    public MoneyDto  ActualInvoiceAmount { get; set; }
    public MoneyDto AmountPaid { get; set; }
    public MoneyDto OutstandingAmount { get; set; }
    public DateTime LastPaymentDate { get; set; }
    public bool HasPendingWalletRequest { get; set; }
    public DateTime InvoiceItemDate { get; set; }
    
    public DateTime? TimeOfInvoicePaid { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public DateTime? ToUpDate { get; set; }
    public InvoiceSource? InvoiceSource { get; set; }
    
    public PaymentStatus? PaymentStatus { get; set; }
    public bool IsServiceOnCredit { get; set; }
    public List<PaymentAllInputResponse>  AllVisits { get; set; }
}