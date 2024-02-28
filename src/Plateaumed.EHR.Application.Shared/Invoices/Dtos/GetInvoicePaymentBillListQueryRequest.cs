using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetInvoicePaymentBillListQueryRequest: PagedAndSortedResultRequestDto
{
    [Required]
    public long PatientId { get; set; }
    
}