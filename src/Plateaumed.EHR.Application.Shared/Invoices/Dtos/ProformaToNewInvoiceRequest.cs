using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos;

public class ProformaToNewInvoiceRequest : EntityDto<long?>
{
    [Required]
    public string InvoiceNo { set; get; }
    [Required]
    public long PatientId { set; get; }
    [Required]
    public MoneyDto TotalAmount { set; get; }
}