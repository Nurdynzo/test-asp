using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllFacilityBanksInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public long? FacilityIdFilter { get; set; }
    }
}