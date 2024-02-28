using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Organizations.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}