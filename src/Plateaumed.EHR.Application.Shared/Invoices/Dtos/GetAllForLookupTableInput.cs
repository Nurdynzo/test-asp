using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}