using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.MultiTenancy.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
