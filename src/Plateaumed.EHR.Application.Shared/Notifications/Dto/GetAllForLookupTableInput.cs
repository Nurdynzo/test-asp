using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Notifications.Dto
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}