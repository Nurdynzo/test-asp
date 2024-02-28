using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetPaymentExpandableQueryRequest: PagedAndSortedResultRequestDto
{
    [Required]
    public long PatientId { get; set; }
}