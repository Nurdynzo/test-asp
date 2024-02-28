using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetInvoiceItemPricingRequest: PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}