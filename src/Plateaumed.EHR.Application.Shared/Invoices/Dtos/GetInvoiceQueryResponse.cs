using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetInvoiceQueryResponse : EntityDto<long?>
{
    public string InvoiceNo { set; get; }
    
    public long AppointmentId { set; get; }
    
    public long PatientId { set; get; }
    
    public string PaymentType { set; get; }
    public MoneyDto TotalAmount { set; get; }
    public bool IsServiceOnCredit { get; set; }

    public IEnumerable<InvoiceItemRequest> Items { get; set; }
}