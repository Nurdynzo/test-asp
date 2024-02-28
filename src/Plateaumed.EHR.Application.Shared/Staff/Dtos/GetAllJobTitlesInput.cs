using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetAllJobTitlesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string ShortNameFilter { get; set; }

        public long? FacilityId { get; set; }

        public bool IncludeLevels { get; set; }
    }
}
