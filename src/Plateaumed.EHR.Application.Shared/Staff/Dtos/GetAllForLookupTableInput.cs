using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}