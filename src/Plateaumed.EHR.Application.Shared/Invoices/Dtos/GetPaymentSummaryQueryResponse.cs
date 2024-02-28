using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetPaymentSummaryQueryResponse: EntityDto<long>
{
    public string Items { get; set; }

    public string InvoiceNo { get; set; }

    public MoneyDto ToUpAmount { get; set; }

    public MoneyDto Amount { get; set; }

    public PaymentTypes? PaymentType { get; set; }

    public MoneyDto AmountPaid { get; set; }

    public MoneyDto OutstandingAmount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public AppointmentStatusType? AppointmentStatus { get; set; }

    public DateTime? PaymentDate { get; set; }

    public InvoiceItemStatus? ItemStatus { get; set; }

    public TransactionAction? ActionStatus { get; set; } 
}