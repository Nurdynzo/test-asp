using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetAllProformaInvoiceQueryResponse: EntityDto<long>
{
    public DateTime CreatedDate { get; set; }
    public string InvoiceNo { get; set; }
    public long PatientId { get; set; }
    public string PaymentType { get; set; }
    public MoneyDto TotalAmount { get; set; }
    public List<InvoiceItemResponse> InvoiceItems { get; set; }
}